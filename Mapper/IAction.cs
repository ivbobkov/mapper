namespace TinyMapper
{
    public interface IAction<in TSource, TReceiver>
    {
        TReceiver Execute(TReceiver receiver, TSource source);
    }
}