using System;
using System.Linq.Expressions;

namespace TinyMapper.Builders
{
    public interface ICanAddIfOrAssign<TSource, TReceiver, TReceiverProperty>
    {
        ICanAddAssign<TSource, TReceiver, TReceiverProperty> If(Expression<Func<TSource, bool>> clause);

        ICanAddResolve<TSource, TReceiver> Assign(Expression<Func<TSource, TReceiverProperty>> statement);

        ICanAddResolve<TSource, TReceiver> Assign(
            Expression<Func<TSource, TReceiverProperty>> statement,
            Expression<Func<TReceiverProperty, TReceiverProperty>> converter);

        ICanAddResolve<TSource, TReceiver> Assign(TReceiverProperty value);
    }
}