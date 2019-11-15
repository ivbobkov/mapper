using System.Collections.Generic;

namespace SampleMapper.Builders
{
    public interface IMappingActionsBuilder<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
        ICanAddThenDo<TSource, TReceiverMember> If(Condition<TSource> executionClause);
        IEnumerable<MappingAction> Build();
    }

    public interface ICanAddThenDo<TSource, TReceiverMember>
    {
        void ThenDo(ValueResolver<TSource, TReceiverMember> resolver);
    }
}