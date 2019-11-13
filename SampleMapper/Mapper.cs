using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SampleMapper.Builders;

namespace SampleMapper
{
    public class Mapper : IMapper
    {
        private readonly List<ProfileMap> _profileMaps = new List<ProfileMap>();

        public TReceiver Map<TSource, TReceiver>(TSource source)
        {
            var mapperFunc = GetMapperFunc<TSource, TReceiver>(source);

            return mapperFunc(source);
        }

        public void LoadProfile(ProfileBase profile)
        {
            // TODO: 1) check for typepair and condition duplicates
            var profileMaps = profile.BuildProfileMaps();
            _profileMaps.AddRange(profileMaps);
        }

        private Func<TSource, TReceiver> GetMapperFunc<TSource, TReceiver>(TSource source)
        {
            var profileMap = ResolveProfileMap<TSource, TReceiver>(source);
            var typePair = profileMap.TypePair;

            var parameter = Expression.Parameter(typePair.SourceType, "source");
            var receiverInstance = Expression.Variable(typePair.ReceiverType, "receiverInstance");

            var expressions = new List<Expression>
            {
                Expression.Assign(receiverInstance, GetCreatorExpression<TReceiver>())
            };

            foreach (var propertyMap in profileMap.PropertyMaps)
            {
                foreach (var mappingAction in propertyMap.MappingActions)
                {
                    if (((Condition<TSource>)mappingAction.ExecutionClause).IsMatch(source))
                    {
                        var sourceValue = Expression.Invoke(mappingAction.ValueResolver.AsLambda(), parameter);
                        var receiverMember = Expression.Property(receiverInstance, propertyMap.ReceiverProperty);
                        expressions.Add(Expression.Assign(receiverMember, sourceValue));
                    }
                }
            }

            expressions.Add(receiverInstance);

            var body = Expression.Block(new[] { receiverInstance }, expressions);
            return Expression.Lambda<Func<TSource, TReceiver>>(body, parameter).Compile();
        }

        private ProfileMap ResolveProfileMap<TSource, TReceiver>(TSource source)
        {
            var typePair = TypePair.Create<TSource, TReceiver>();
            var profileMaps = _profileMaps.Where(x => x.TypePair.Equals(typePair)).ToList();

            if (!profileMaps.Any())
            {
                throw new InvalidOperationException("No profiles added for given type pair");
            }

            var profilesMatchedByCondition = profileMaps
                .Where(c => ((Condition<TSource>)c.Condition).IsMatch(source) && !c.IsDefault)
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

        // to profile?
        private NewExpression GetCreatorExpression<TReceiver>()
        {
            var receiverType = typeof(TReceiver);
            var receiverConstructor = receiverType.GetConstructor(Array.Empty<Type>());

            if (receiverConstructor == null)
            {
                throw new Exception($"Default constructor for {receiverType.Name} not found");
            }

            var newExpression = Expression.New(receiverConstructor);

            return newExpression;
        }
    }
}