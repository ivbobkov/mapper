using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public interface IValueResolver
    {
        LambdaExpression AsLambda();
    }

    public abstract class ValueResolver<TSource, TReceiverMember> : IValueResolver
    {
        private Expression<Func<TSource, TReceiverMember>> _resolverExpression;

        public LambdaExpression AsLambda()
        {
            if (_resolverExpression == null)
            {
                _resolverExpression = CreateResolver();
            }

            return _resolverExpression;
        } 

        protected abstract Expression<Func<TSource, TReceiverMember>> CreateResolver();
    }
}