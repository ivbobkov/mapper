using System;
using System.Linq.Expressions;

namespace SampleMapper.Internal
{
    internal class OrCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _left;
        private readonly Condition<TSource> _right;

        public OrCondition(Condition<TSource> left, Condition<TSource> right)
        {
            _left = left;
            _right = right;
        }

        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            var leftExpression = _left.AsLambda();
            var rightExpression = _right.AsLambda();

            var parameter = Expression.Parameter(typeof(TSource));
            var leftVisitor = new ParameterReplacerVisitor(leftExpression.Parameters[0], parameter);
            var normalizedLeftExpression = leftVisitor.Visit(leftExpression.Body);

            var rightVisitor = new ParameterReplacerVisitor(rightExpression.Parameters[0], parameter);
            var normalizedRightExpression = rightVisitor.Visit(rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(
                Expression.OrElse(normalizedLeftExpression, normalizedRightExpression),
                parameter);
        }

        public override int GetHashCode()
        {
            return _left.GetHashCode() | _right.GetHashCode();
        }
    }
}