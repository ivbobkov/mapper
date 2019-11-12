using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleMapper
{
    // TODO: make generic
    public abstract class ProfilesBuilder<TSource, TReceiver>
    {
        private readonly HashSet<ProfileMap> _profileMaps = new HashSet<ProfileMap>();

        protected IProfileConfigurator<TSource, TReceiver> CreateProfile()
        {
            var typeMap

            return null;
        }
    }
}