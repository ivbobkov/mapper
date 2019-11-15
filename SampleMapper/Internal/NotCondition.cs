using System;
using System.Linq.Expressions;

namespace SampleMapper.Internal
{
    internal class NotCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _condition;

        public NotCondition(Condition<TSource> condition)
        {
            _condition = condition;
        }

        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            var clauseExpression = _condition.AsLambda();
            var body = Expression.Not(clauseExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(body, clauseExpression.Parameters[0]);
        }

        public override int GetHashCode()
        {
            return ~_condition.GetHashCode();
        }
    }
}