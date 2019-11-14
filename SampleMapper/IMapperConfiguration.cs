using System;
using SampleMapper.Builders;

namespace SampleMapper
{
    public interface IMapperConfiguration
    {
        void LoadProfile(ProfileBase profile);
        ProfileMap GetProfileMap<TSource, TReceiver>(TSource source);
        Func<TSource, TReceiver> GetMapperFunc<TSource, TReceiver>(TSource source);
    }
}