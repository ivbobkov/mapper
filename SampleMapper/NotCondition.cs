using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class NotCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _condition;

        public NotCondition(Condition<TSource> condition)
        {
            _condition = condition;
        }

        public override Expression<Func<TSource, bool>> CreateCondition()
        {
            var clauseExpression = _condition.CreateCondition();
            var parameter = clauseExpression.Parameters.Single();
            var body = Expression.Not(clauseExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }
    }
}