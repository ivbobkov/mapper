using System;
using System.Linq.Expressions;

namespace SampleMapper.Tests.Fakes
{
    public class ExpressionResolver<TSource, TReceiverMember> : ValueResolver<TSource, TReceiverMember>
    {
        private readonly Expression<Func<TSource, TReceiverMember>> _expression;

        public ExpressionResolver(Expression<Func<TSource, TReceiverMember>> expression)
        {
            _expression = expression;
        }

        protected override Expression<Func<TSource, TReceiverMember>> CreateResolver()
        {
            return _expression;
        }
    }
}