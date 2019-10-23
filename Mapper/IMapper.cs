namespace TinyMapper
{
    public interface IMapper<TSource, TReceiver>
    {
        TReceiver Map(TSource source);
    }
}