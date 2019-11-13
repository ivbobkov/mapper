using System.Collections.Generic;

namespace SampleMapper.Builders
{
    public interface IMappingActionsBuilder<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
        ICanAddDo<TSource, TReceiverMember> If(Condition<TSource> executionClause);
        IEnumerable<MappingAction> Build();
    }

    public interface ICanAddDo<TSource, TReceiverMember>
    {
        void Do(ValueResolver<TSource, TReceiverMember> resolver);
    }
}