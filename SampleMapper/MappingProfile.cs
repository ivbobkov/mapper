using System;
using System.Collections.Generic;
using SampleMapper.Builders;

namespace SampleMapper
{
    public class MappingProfile
    {
        private readonly List<IProfileMapBuilder> _profileMapBuilders = new List<IProfileMapBuilder>();

        public IProfileMapBuilder<TSource, TReceiver> CreateProfile<TSource, TReceiver>()
        {
            var profileMapBuilder = new ProfileMapBuilder<TSource, TReceiver>();
            _profileMapBuilders.Add(profileMapBuilder);

            return profileMapBuilder;
        }

        public IEnumerable<ProfileMap> BuildProfileMaps()
        {
            var profilesToReturn = new Dictionary<int, ProfileMap>();

            foreach (var profileMapBuilder in _profileMapBuilders)
            {
                var profileMap = profileMapBuilder.Build();
                var identity = profileMap.Identity;

                if (profilesToReturn.ContainsKey(identity))
                {
                    var message = GetErrorMessageForProfileDuplication(profileMap);
                    throw new InvalidOperationException($"There is duplicate for: {message}");
                }

                profilesToReturn.Add(identity, profileMap);
            }

            return profilesToReturn.Values;
        }

        private string GetErrorMessageForProfileDuplication(ProfileMap profileMap)
        {
            var typePair = profileMap.TypePair;

            return $"<{typePair.SourceType}, {typePair.ReceiverType}> IsDefault {profileMap.IsDefault}";
        }
    }
}