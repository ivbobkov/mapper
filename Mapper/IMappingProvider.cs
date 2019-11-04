using System;

namespace TinyMapper
{
    public interface IMappingProvider
    {
        void RegisterProfile(MappingProfileBase profile);
        Func<TSource, TReceiver> GetTypedMappingFunc<TSource, TReceiver>();
    }
}