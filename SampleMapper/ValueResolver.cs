using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class ValueResolver<TSource, TReceiverMember> : IValueResolver
    {
        protected ValueResolver()
        {
            ResolverExpression = CreateResolver();
        }

        protected Expression<Func<TSource, TReceiverMember>> ResolverExpression { get; }

        public LambdaExpression AsLambda() => ResolverExpression;

        protected abstract Expression<Func<TSource, TReceiverMember>> CreateResolver();
    }
}