using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class ValueResolver<TSource, TReceiverMember> : IValueResolver
    {
        private readonly Lazy<Expression<Func<TSource, TReceiverMember>>> _resolverExpression;

        protected ValueResolver()
        {
            _resolverExpression = new Lazy<Expression<Func<TSource, TReceiverMember>>>(() => CreateResolver());
        }

        public LambdaExpression AsLambda() => _resolverExpression.Value;

        protected abstract Expression<Func<TSource, TReceiverMember>> CreateResolver();
    }
}