using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public interface IProfileMapBuilder<TSource, TReceiver>
    {
        IProfileMapBuilder<TSource, TReceiver> Condition(Condition<TSource> condition);

        IProfileMapBuilder<TSource, TReceiver> Include(params PropertyMap[] propertyMaps);

        IProfileMapBuilder<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> receiverMember,
            Action<IMemberMapsBuilder<TSource, TReceiverMember>> configuratorAction);
    }
}