using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public interface IProfileConfigurator<TSource, TReceiver>
    {
        IProfileConfigurator<TSource, TReceiver> Condition(Condition<TSource> condition);

        IProfileConfigurator<TSource, TReceiver> Include(params PropertyMap[] propertyMaps);

        IProfileConfigurator<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> receiverMember,
            Action<IPropertyConfigurator<TSource, TReceiverMember>> configurator);
    }
}