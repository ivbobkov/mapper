namespace SampleMapper
{
    public interface ICanAddResolve<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
    }
}