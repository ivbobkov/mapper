using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public interface ICondition
    {
        LambdaExpression AsLambda();
        int GetHashCode();
    }

    public abstract class Condition<TSource> : ICondition
    {
        private Expression<Func<TSource, bool>> _conditionExpression;
        private Func<TSource, bool> _conditionFunc;

        public LambdaExpression AsLambda()
        {
            SetConditionState();

            return _conditionExpression;
        }

        public bool IsMatch(TSource source)
        {
            SetConditionState();

            return _conditionFunc(source);
        }

        protected abstract Expression<Func<TSource, bool>> CreateCondition();

        private void SetConditionState()
        {
            if (_conditionExpression == null)
            {
                _conditionExpression = CreateCondition();
                _conditionFunc = _conditionExpression.Compile();
            }
        }

        public static Condition<TSource> operator &(Condition<TSource> left, Condition<TSource> right)
        {
            return new AndCondition<TSource>(left, right);
        }

        public static Condition<TSource> operator |(Condition<TSource> left, Condition<TSource> right)
        {
            return new OrCondition<TSource>(left, right);
        }

        public static Condition<TSource> operator !(Condition<TSource> condition)
        {
            return new NotCondition<TSource>(condition);
        }

        public abstract override int GetHashCode();

        public override bool Equals(object obj)
        {
            var inputCondition = obj as ICondition;

            if (inputCondition == null)
            {
                return false;
            }

            return GetHashCode() == inputCondition.GetHashCode();
        }
    }

    public class AndCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _left;
        private readonly Condition<TSource> _right;

        public AndCondition(Condition<TSource> left, Condition<TSource> right)
        {
            _left = left;
            _right = right;
        }

        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            var leftExpression = _left.AsLambda();
            var rightExpression = _right.AsLambda();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(andExpression, leftExpression.Parameters.Single());
        }

        public override int GetHashCode()
        {
            return _left.GetHashCode() & _right.GetHashCode();
        }
    }

    public class OrCondition<TSource> : Condition<TSource>
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

            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(orExpression, leftExpression.Parameters.Single());
        }

        public override int GetHashCode()
        {
            return _left.GetHashCode() | _right.GetHashCode();
        }
    }

    public class NotCondition<TSource> : Condition<TSource>
    {
        private readonly Condition<TSource> _condition;

        public NotCondition(Condition<TSource> condition)
        {
            _condition = condition;
        }

        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            var clauseExpression = _condition.AsLambda();
            var parameter = clauseExpression.Parameters.Single();
            var body = Expression.Not(clauseExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }

        public override int GetHashCode()
        {
            return ~_condition.GetHashCode();
        }
    }

    public class BlankCondition<TSource> : Condition<TSource>
    {
        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            return _ => true;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }

    public static class ConditionExtensions
    {
        public static Condition<TSource> And<TSource>(this Condition<TSource> source, Condition<TSource> condition)
        {
            return new AndCondition<TSource>(source, condition);
        }

        public static Condition<TSource> Or<TSource>(this Condition<TSource> source, Condition<TSource> condition)
        {
            return new OrCondition<TSource>(source, condition);
        }

        public static Condition<TSource> Not<TSource>(this Condition<TSource> source)
        {
            return new NotCondition<TSource>(source);
        }
    }
}