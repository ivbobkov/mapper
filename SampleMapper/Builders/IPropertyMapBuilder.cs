using System;
using System.Linq.Expressions;

namespace SampleMapper.Builders
{
    public interface IPropertyMapBuilder<TSource, TReceiver, TReceiverMember>
    {
        IPropertyMapBuilder<TSource, TReceiver, TReceiverMember> AddMember(
            Expression<Func<TReceiver, TReceiverMember>> receiverMember);

        IPropertyMapBuilder<TSource, TReceiver, TReceiverMember> AddSetupAction(
            Action<IMappingActionsBuilder<TSource, TReceiverMember>> setupAction);

        PropertyMap Build();
    }
}