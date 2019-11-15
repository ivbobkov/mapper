using System.Collections.Generic;
using System.Linq;

namespace SampleMapper.Builders
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
            var profileMaps = _profileMapBuilders.Select(x => x.Build());
            // TODO: assertions

            return profileMaps;
        }
    }
}