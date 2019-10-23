using System;
using System.Linq.Expressions;

namespace TinyMapper.Builders
{
    public interface IMappingBuilder<TSource, TReceiver>
    {
        IMappingBuilder<TSource, TReceiver> ExecuteIf(Expression<Func<TSource, bool>> clause);

        IMappingBuilder<TSource, TReceiver> Include(params IAction<TSource, TReceiver>[] actions);

        IMappingBuilder<TSource, TReceiver> ActionFor<TReceiverProperty>(
            Expression<Func<TReceiver, TReceiverProperty>> receiverSelector,
            Func<IActionBuilder<TSource, TReceiver, TReceiverProperty>, ICanAddResolve<TSource, TReceiver>> buildFunction);

        IMappingBuilder<TSource, TReceiver> AfterMap(Action<TSource, TReceiver> afterMap);

        IMapping<TSource, TReceiver> Build();
    }
}