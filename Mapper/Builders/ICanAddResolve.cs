namespace TinyMapper.Builders
{
    public interface ICanAddResolve<in TSource, TReceiver>
    {
        IAction<TSource, TReceiver> Resolve();
    }
}