namespace TinyMapper.Builders
{
    public interface ICanAddElse<TSource, TReceiver, TReceiverProperty>
    {
        ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty> Else { get; }
    }
}