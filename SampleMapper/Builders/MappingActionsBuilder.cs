using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMapper.Builders
{
    public class MappingActionsBuilder<TSource, TReceiverMember> :
        IMappingActionsBuilder<TSource, TReceiverMember>,
        ICanAddThenDo<TSource, TReceiverMember>
    {
        private readonly Dictionary<ICondition, IValueResolver> _state = new Dictionary<ICondition, IValueResolver>();

        void IMappingActionsBuilder<TSource, TReceiverMember>.Do(ValueResolver<TSource, TReceiverMember> resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            var executionClause = new BlankCondition<TSource>();
            AssertIfExecutionClauseExists(executionClause);

            _state.Add(executionClause, resolver);
        }

        ICanAddThenDo<TSource, TReceiverMember> IMappingActionsBuilder<TSource, TReceiverMember>.If(Condition<TSource> executionClause)
        {
            if (executionClause == null)
            {
                throw new ArgumentNullException(nameof(executionClause));
            }

            AssertIfLastActionIsNotConfigured();
            AssertIfExecutionClauseExists(executionClause);
            _state.Add(executionClause, null);

            return this;
        }

        public IEnumerable<MappingAction> Build()
        {
            if (!_state.Any())
            {
                throw new InvalidOperationException("No mapping actions configured");
            }

            var last = _state.Last();

            if (!(last.Key is BlankCondition<TSource>))
            {
                throw new InvalidOperationException("Conditional actions configured, but default at the end is not");
            }

            foreach (var state in _state)
            {
                yield return new MappingAction(state.Key, state.Value);
            }
        }

        void ICanAddThenDo<TSource, TReceiverMember>.ThenDo(ValueResolver<TSource, TReceiverMember> resolver)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            if (_state.Count == 0)
            {
                throw new InvalidOperationException("No mapping actions were configured");
            }

            var lastEntry = _state.Last();

            if (lastEntry.Value != null)
            {
                throw new InvalidOperationException("Last mapping action was configured");
            }

            _state[lastEntry.Key] = resolver;
        }

        private void AssertIfLastActionIsNotConfigured()
        {
            if (_state.Count == 0)
            {
                return;
            }

            var lastEntry = _state.Last();

            if (lastEntry.Value == null)
            {
                throw new InvalidOperationException("Last mapping action is not configured");
            }
        }

        private void AssertIfExecutionClauseExists(ICondition executionClause)
        {
            if (_state.ContainsKey(executionClause))
            {
                throw new InvalidOperationException("There is execution clause duplicate");
            }
        }
    }
}