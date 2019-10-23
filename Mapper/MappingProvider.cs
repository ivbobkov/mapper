using System;

namespace TinyMapper
{
    public class MappingProvider<TSource, TReceiver> : IMappingProvider<TSource, TReceiver>
    {
        public void RegisterProfile(MappingProfileBase<TSource, TReceiver> profile)
        {
            throw new NotImplementedException();
        }

        public Func<TSource, TReceiver> GetMapperFunc()
        {
            throw new NotImplementedException();
        }
    }
}