namespace TinyMapper
{
    public interface IMapper
    {
        TReceiver Map<TSource, TReceiver>(TSource source);
    }
}