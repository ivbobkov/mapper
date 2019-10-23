using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TinyMapper.Core;

namespace TinyMapper.Builders
{
    public class MappingBuilder<TSource, TReceiver> : IMappingBuilder<TSource, TReceiver>
    {
        private Expression<Func<TSource, bool>> _executionClause = _ => true;
        private readonly List<IAction<TSource, TReceiver>> _actions = new List<IAction<TSource, TReceiver>>();
        private Action<TSource, TReceiver> _afterMap = (_, __) => { };

        public IMappingBuilder<TSource, TReceiver> ExecuteIf(IClause<TSource> clause)
        {
            throw new NotImplementedException();
        }

        public IMappingBuilder<TSource, TReceiver> Include(params IAction<TSource, TReceiver>[] actions)
        {
            if (actions == null)
            {
                throw new ArgumentNullException(nameof(actions));
            }

            _actions.AddRange(actions);

            return this;
        }

        public IMappingBuilder<TSource, TReceiver> For<TReceiverProperty>(
            Expression<Func<TReceiver, TReceiverProperty>> receiverSelector,
            Func<IActionBuilder<TSource, TReceiver, TReceiverProperty>, ICanAddResolve<TSource, TReceiver>> buildFunction)
        {
            var builder = new ActionBuilder<TSource, TReceiver, TReceiverProperty>(receiverSelector);
            var action = buildFunction(builder).Resolve();
            _actions.Add(action);

            return this;
        }

        public IMappingBuilder<TSource, TReceiver> AfterMap(Action<TSource, TReceiver> afterMap)
        {
            _afterMap = afterMap ?? throw new ArgumentNullException(nameof(afterMap));

            return this;
        }

        public IMapping<TSource, TReceiver> Build()
        {
            return new Mapping<TSource, TReceiver>(
                _executionClause,
                _actions,
                _afterMap);
        }
    }
}