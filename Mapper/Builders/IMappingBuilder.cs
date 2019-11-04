using System;
using System.Linq.Expressions;
using TinyMapper.Core;

namespace TinyMapper.Builders
{
    public interface IMappingBuilder<TSource, TReceiver>
    {
        IMappingBuilder<TSource, TReceiver> ExecuteIf<TClause>(TClause clause) where TClause : IClause<TSource, TClause>;

        IMappingBuilder<TSource, TReceiver> Include(params IAction<TSource, TReceiver>[] actions);

        IMappingBuilder<TSource, TReceiver> For<TReceiverProperty>(
            Expression<Func<TReceiver, TReceiverProperty>> receiverSelector,
            Func<IActionBuilder<TSource, TReceiver, TReceiverProperty>, ICanAddResolve<TSource, TReceiver>> buildFunction);

        IMappingBuilder<TSource, TReceiver> AfterMap(Action<TSource, TReceiver> afterMap);

        IMapping<TSource, TReceiver> Build();
    }
}