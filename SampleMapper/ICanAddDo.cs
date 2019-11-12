namespace SampleMapper
{
    public interface ICanAddDo<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
    }
}