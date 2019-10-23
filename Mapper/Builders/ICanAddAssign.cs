using System;
using System.Linq.Expressions;

namespace TinyMapper.Builders
{
    public interface ICanAddAssign<TSource, TReceiver, TReceiverProperty>
    {
        ICanAddElse<TSource, TReceiver, TReceiverProperty> Assign(
            Expression<Func<TSource, TReceiverProperty>> statement);

        ICanAddElse<TSource, TReceiver, TReceiverProperty> Assign(
            Expression<Func<TSource, TReceiverProperty>> statement,
            Expression<Func<TReceiverProperty, TReceiverProperty>> converter);

        ICanAddElse<TSource, TReceiver, TReceiverProperty> Assign(TReceiverProperty value);
    }
}