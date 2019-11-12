using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class ValueResolver<TSource, TReceiverMember>
    {
        public abstract Expression<Func<TSource, TReceiverMember>> ToExpression();
    }
}