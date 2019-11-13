using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

            throw new NotImplementedException();
        }

        public void LoadProfile(ProfileBase profile)
        {
        }

        private Func<TSource, TReceiver> GetMapperFunc<TSource, TReceiver>(TSource source)
        {
            var profileMap = ResolveProfileMap<TSource, TReceiver>(source);


            return x => default;
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
                .Where(c => ((Condition<TSource>) c.Condition).IsMatch(source))
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
    }
}