namespace SampleMapper
{
    public interface IPropertyConfigurator<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
        ICanAddResolve<TSource, TReceiverMember> If(Condition<TSource> condition);
    }
}