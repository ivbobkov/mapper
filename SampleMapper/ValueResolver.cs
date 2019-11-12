using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class ValueResolver<TSource, TReceiverMember> : IValueResolver
    {
        public abstract Expression<Func<TSource, TReceiverMember>> ToExpression();
        LambdaExpression IValueResolver.ToLambda() => ToExpression();
    }
}