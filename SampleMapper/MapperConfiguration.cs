using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class MapperConfiguration : IMapperConfiguration
    {
        private readonly IDictionary<TypePair, List<ProfileMap>> _profileMaps =
            new Dictionary<TypePair, List<ProfileMap>>();

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
            var profileMaps = _profileMaps[typePair].Where(x => x.TypePair.Equals(typePair)).ToList();

            if (!profileMaps.Any())
            {
                throw new InvalidOperationException("No profiles added for given type pair");
            }

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

        // TODO: cache it
        public Func<TSource, TReceiver> GetMapperFunc<TSource, TReceiver>(TSource source)
        {
            var profileMap = GetProfileMap<TSource, TReceiver>(source);
            var typePair = profileMap.TypePair;

            if (_mapperFunctions.ContainsKey(profileMap.Identity))
            {
            }

            return CreateMapperFunc<TSource, TReceiver>(profileMap);
        }

        private Func<TSource, TReceiver> CreateMapperFunc<TSource, TReceiver>(ProfileMap profileMap)
        {
            Expression<Func<TSource, PropertyMap, LambdaExpression>> valueResolverExpression =
                (source, propertyMap) => propertyMap.MappingActions
                    .Single(x => ((Condition<TSource>) x.ExecutionClause).IsMatch(source))
                    .ValueResolver
                    .AsLambda();

            var sourceParameter = Expression.Parameter(typeof(TSource), "source");
            //var profileMapParameter = Expression.Parameter(typeof(ProfileMap), "profileMap");
            var receiverVariable = Expression.Variable(typeof(TReceiver), "receiver");

            var expressions = new List<Expression>
            {
                Expression.Assign(receiverVariable, Expression.New(profileMap.ReceiverConstructorInfo))
            };

            // TODO: check in expression
            foreach (var propertyMap in profileMap.PropertyMaps)
            {
                var propertyMapVariable = Expression.Variable(typeof(PropertyMap));
                expressions.Add(Expression.Assign(propertyMapVariable, Expression.Constant(propertyMap)));

                var check0 = Expression.Block(
                    new[] {sourceParameter, propertyMapVariable},
                    valueResolverExpression
                );

                //InvocationExpression resolverExpression = Expression.Invoke(
                //    valueResolverExpression,
                //    sourceParameter,
                //    propertyMapVariable);

                //var temp = Expression.Variable(typeof(LambdaExpression));
                //var t1 = Expression.Assign(temp, resolverExpression);
                //expressions.Add(resolverExpression);

                //var sourceValue = Expression.Call(resolverExpression, sourceParameter);
                var sourceValue = Expression.Invoke(check0, sourceParameter, propertyMapVariable);
                var receiverMember = Expression.Property(receiverVariable, propertyMap.ReceiverProperty);
                expressions.Add(Expression.Assign(receiverMember, sourceValue));

                //var sourceValue = Expression.Invoke(mappingAction.ValueResolver.AsLambda(), sourceParameter);
                //var receiverMember = Expression.Property(receiverVariable, propertyMap.ReceiverProperty);
                //expressions.Add(Expression.Assign(receiverMember, sourceValue));

                //foreach (var mappingAction in propertyMap.MappingActions)
                //{
                //    if (((Condition<TSource>)mappingAction.ExecutionClause).IsMatch(source))
                //    {
                //        var sourceValue = Expression.Invoke(mappingAction.ValueResolver.AsLambda(), sourceParameter);
                //        var receiverMember = Expression.Property(receiverInstance, propertyMap.ReceiverProperty);
                //        expressions.Add(Expression.Assign(receiverMember, sourceValue));
                //    }
                //}

            }

            expressions.Add(receiverVariable);

            //var body = Expression.Block(new[] { receiverVariable }, expressions);
            var body = Expression.Block(new[] { receiverVariable }, expressions);
            return Expression.Lambda<Func<TSource, TReceiver>>(body, sourceParameter).Compile();
        }
    }
}