using System;

namespace TinyMapper
{
    public class MappingProvider : IMappingProvider
    {
        public void RegisterProfile(MappingProfileBase profile)
        {
            throw new NotImplementedException();
        }

        public Func<TSource, TReceiver> GetTypedMappingFunc<TSource, TReceiver>()
        {
            throw new NotImplementedException();
        }
    }
}