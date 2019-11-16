using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class MapperConfiguration : IMapperConfiguration
    {
        // TODO: to concurent ?
        private readonly IDictionary<TypePair, List<ProfileMap>> _profileMaps = new Dictionary<TypePair, List<ProfileMap>>();
        private readonly IDictionary<int, Delegate> _mapperFunctions = new Dictionary<int, Delegate>();

        public void LoadProfile(MappingProfile mappingProfile)
        {
            var profileMaps = mappingProfile.BuildProfileMaps();

            foreach (var profileMap in profileMaps)
            {
                var typePair = profileMap.TypePair;

                if (!_profileMaps.ContainsKey(typePair))
                {
                    _profileMaps.Add(typePair, new List<ProfileMap>());
                }

                _profileMaps[typePair].Add(profileMap);
            }
        }

        public ProfileMap GetProfileMap<TSource, TReceiver>(TSource source)
        {
            var typePair = TypePair.Create<TSource, TReceiver>();

            if (!_profileMaps.ContainsKey(typePair))
            {
                throw new InvalidOperationException("No profiles added for given type pair");
            }

            var profileMaps = _profileMaps[typePair]
                .Where(x => x.TypePair.Equals(typePair))
                .ToList();

            var profilesMatchedByCondition = profileMaps
                .Where(c => ((Condition<TSource>)c.ExecutionClause).IsMatch(source) && !c.IsDefault)
                .ToList();

            if (profilesMatchedByCondition.Count == 1)
            {
                return profilesMatchedByCondition.Single();
            }

            if (profilesMatchedByCondition.Count > 1)
            {
                throw new InvalidOperationException("Matched more than one condition");
            }

            var defaultProfiles = profileMaps.Where(x => x.IsDefault).ToList();

            if (defaultProfiles.Count == 1)
            {
                return defaultProfiles.Single();
            }

            if (defaultProfiles.Count > 1)
            {
                throw new InvalidOperationException("Count of fallback profiles more than one");
            }

            throw new InvalidOperationException("No fallback profile");
        }

        public Func<TSource, TReceiver> GetMapperFunc<TSource, TReceiver>(TSource source)
        {
            var profileMap = GetProfileMap<TSource, TReceiver>(source);

            if (_mapperFunctions.TryGetValue(profileMap.Identity, out var mapperFunc))
            {
                return (Func<TSource, TReceiver>) mapperFunc;
            }

            var compiledFunc = CompileMapperFunc<TSource, TReceiver>(profileMap);
            _mapperFunctions.Add(profileMap.Identity, compiledFunc);

            return compiledFunc;
        }

        private static Func<TSource, TReceiver> CompileMapperFunc<TSource, TReceiver>(ProfileMap profileMap)
        {
            var expressions = new List<Expression>();
            var sourceParameter = Expression.Parameter(typeof(TSource));
            var receiverVariable = Expression.Variable(typeof(TReceiver));

            expressions.Add(Expression.Assign(receiverVariable, Expression.New(profileMap.ReceiverConstructorInfo)));

            foreach (var propertyMap in profileMap.PropertyMaps)
            {
                var receiverProperty = Expression.Property(receiverVariable, propertyMap.ReceiverProperty);
                var toNextPropertyLabel = Expression.Label();

                foreach (var mappingAction in propertyMap.MappingActions)
                {
                    var resolvedMemberValue = Expression.Invoke(mappingAction.ValueResolver.AsLambda(), sourceParameter);

                    var ifThen = Expression.IfThen(
                        Expression.Invoke(mappingAction.ExecutionClause.AsLambda(), sourceParameter),
                        Expression.Block(
                            Expression.Assign(receiverProperty, resolvedMemberValue),
                            Expression.Goto(toNextPropertyLabel)
                        )
                    );

                    expressions.Add(ifThen);
                }

                expressions.Add(Expression.Label(toNextPropertyLabel));
            }

            expressions.Add(receiverVariable);
            var body = Expression.Block(new[] { receiverVariable }, expressions);
            var lambda = Expression.Lambda<Func<TSource, TReceiver>>(body, sourceParameter);

            return lambda.Compile();
        }
    }
}