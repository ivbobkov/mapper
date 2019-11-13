using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class ValueResolver<TSource, TReceiverMember> : IValueResolver
    {
        public LambdaExpression AsLambda() => CreateResolver();

        protected abstract Expression<Func<TSource, TReceiverMember>> CreateResolver();
    }
}