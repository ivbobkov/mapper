using System.Collections.Generic;
using SampleMapper.Configurators;

namespace SampleMapper
{
    public abstract class ProfilesBuilder<TSource, TReceiver>
    {
        private readonly List<IProfileMapBuilder<TSource, TReceiver>> _profileMapConfigurators
            = new List<IProfileMapBuilder<TSource, TReceiver>>();

        protected IProfileMapBuilder<TSource, TReceiver> CreateProfile()
        {
            var profileConfigurator = new ProfileMapBuilder<TSource, TReceiver>();
            _profileMapConfigurators.Add(profileConfigurator);

            return profileConfigurator;
        }
    }
}