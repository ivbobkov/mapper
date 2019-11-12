namespace SampleMapper
{
    public interface IMemberMapsBuilder<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
        ICanAddDo<TSource, TReceiverMember> If(Condition<TSource> condition);
    }
}