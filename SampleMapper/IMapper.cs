namespace SampleMapper
{
    public interface IMapper
    {
        TReceiver Map<TSource, TReceiver>(TSource source);
    }
}