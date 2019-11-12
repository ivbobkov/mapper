using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class ProfileConfigurator
    {
        protected IProfileConfigurator<TSource, TReceiver> Define<TSource, TReceiver>()
        {
            return null;
        }
    }

    public interface IProfileConfigurator<TSource, TReceiver>
    {
        IProfileConfigurator<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> receiverMember,
            Action<IPropertyConfigurator<TSource, TReceiverMember>> configurator
        );
        IProfileConfigurator<TSource, TReceiver> Include();
        IProfileConfigurator<TSource, TReceiver> Condition(Condition<TSource> condition);
    }

    public interface IPropertyConfigurator<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
        ICanAddResolve<TSource, TReceiverMember> If(Condition<TSource> condition);
    }

    public interface ICanAddResolve<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
    }
}