using System;
using System.Linq.Expressions;

namespace SampleMapper.Builders
{
    public interface IProfileMapBuilder
    {
        ProfileMap Build();
    }

    public interface IProfileMapBuilder<TSource, TReceiver> : IProfileMapBuilder
    {
        IProfileMapBuilder<TSource, TReceiver> UseExecutionClause(Condition<TSource> executionClause);

        IProfileMapBuilder<TSource, TReceiver> UseAsDefaultProfile();

        IProfileMapBuilder<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> member,
            Action<IMappingActionsBuilder<TSource, TReceiverMember>> setupAction);
    }
}