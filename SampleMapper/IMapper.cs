using SampleMapper.Builders;

namespace SampleMapper
{
    public interface IMapper
    {
        TReceiver Map<TSource, TReceiver>(TSource source);
        void LoadProfile(ProfileBase profile);
    }
}