using System;
using System.Collections.Generic;

namespace SampleMapper.Builders
{
    public class MappingActionsBuilder<TSource, TReceiverMember> :
        IMappingActionsBuilder<TSource, TReceiverMember>,
        ICanAddDo<TSource, TReceiverMember>
    {
        private Condition<TSource> _currentExecutionClause;
        private readonly List<MappingAction> _mappingActions = new List<MappingAction>();

        void IMappingActionsBuilder<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            var mappingAction = new MappingAction(new BlankCondition<TSource>(), resolver);
            _mappingActions.Add(mappingAction);
        }

        ICanAddDo<TSource, TReceiverMember> IMappingActionsBuilder<TSource, TReceiverMember>.If(Condition<TSource> executionClause)
        {
            _currentExecutionClause = executionClause;

            return this;
        }

        public IEnumerable<MappingAction> Build()
        {
            // TODO:
            // 1) Assert if last element is not BlankCondition
            // 2) Assert if there is condition duplicates
            throw new NotImplementedException();
        }

        void ICanAddDo<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            if (_currentExecutionClause == null)
            {
                throw new InvalidOperationException("You must add condition before adding resolver");
            }

            var mappingAction = new MappingAction(_currentExecutionClause, resolver);
            _mappingActions.Add(mappingAction);
        }
    }
}