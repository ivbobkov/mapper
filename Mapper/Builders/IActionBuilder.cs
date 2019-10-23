using System;
using System.Linq.Expressions;

namespace TinyMapper.Builders
{
    public interface IActionBuilder<TSource, TReceiver, TReceiverProperty>
    {
        ICanAddResolve<TSource, TReceiver> Assign(
            Expression<Func<TSource, TReceiverProperty>> statement,
            Expression<Func<TReceiverProperty, TReceiverProperty>> converter);

        ICanAddResolve<TSource, TReceiver> Assign(TReceiverProperty value);

        ICanAddAssign<TSource, TReceiver, TReceiverProperty> If(Expression<Func<TSource, bool>> clause);
    }
}