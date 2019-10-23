using System;

namespace TinyMapper
{
    public interface IMappingProvider<TSource, TReceiver>
    {
        void RegisterProfile(MappingProfileBase<TSource, TReceiver> profile);
        Func<TSource, TReceiver> GetMapperFunc();
    }
}