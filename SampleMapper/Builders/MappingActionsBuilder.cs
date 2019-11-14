using System;
using System.Collections.Generic;

namespace SampleMapper.Builders
{
    public class MappingActionsBuilder<TSource, TReceiverMember> :
        IMappingActionsBuilder<TSource, TReceiverMember>,
        ICanAddDo<TSource, TReceiverMember>
    {
        private Condition<TSource> _currentExecutionClause;
        private readonly HashSet<MappingAction> _mappingActions = new HashSet<MappingAction>();

        void IMappingActionsBuilder<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            if (_currentExecutionClause == null)
            {
                throw new InvalidOperationException();
            }

            var mappingAction = new MappingAction(new BlankCondition<TSource>(), resolver);

            if (_mappingActions.Contains(mappingAction))
            {
                throw new InvalidOperationException();
            }

            _mappingActions.Add(mappingAction);
        }

        ICanAddDo<TSource, TReceiverMember> IMappingActionsBuilder<TSource, TReceiverMember>.If(Condition<TSource> executionClause)
        {
            if (_currentExecutionClause != null)
            {
                throw new InvalidOperationException();
            }

            _currentExecutionClause = executionClause;

            return this;
        }

        public IEnumerable<MappingAction> Build()
        {
            return _mappingActions;
        }

        void ICanAddDo<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            if (_currentExecutionClause == null)
            {
                throw new InvalidOperationException("You must add condition before adding resolver");
            }

            var mappingAction = new MappingAction(_currentExecutionClause, resolver);

            if (_mappingActions.Contains(mappingAction))
            {
                throw new InvalidOperationException();
            }

            _mappingActions.Add(mappingAction);
            _currentExecutionClause = null;
        }
    }
}