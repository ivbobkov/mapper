using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class AndCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _left;
        private readonly Condition<TSource> _right;

        public AndCondition(Condition<TSource> left, Condition<TSource> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TSource, bool>> CreateCondition()
        {
            var leftExpression = _left.CreateCondition();
            var rightExpression = _right.CreateCondition();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
}