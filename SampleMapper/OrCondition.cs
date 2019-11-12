using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class OrCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _left;
        private readonly Condition<TSource> _right;

        public OrCondition(Condition<TSource> left, Condition<TSource> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TSource, bool>> CreateCondition()
        {
            var leftExpression = _left.CreateCondition();
            var rightExpression = _right.CreateCondition();

            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }
}