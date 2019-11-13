using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public interface ICondition
    {
        LambdaExpression AsLambda();
    }

    public abstract class Condition<TSource> : ICondition
    {
        protected Condition()
        {
            ConditionExpression = CreateCondition();
            CompiledConditionExpression = ConditionExpression.Compile();
        }

        protected Expression<Func<TSource, bool>> ConditionExpression { get; }
        protected Func<TSource, bool> CompiledConditionExpression { get; }

        public LambdaExpression AsLambda() => ConditionExpression;

        public bool IsMatch(TSource source)
        {
            return CompiledConditionExpression(source);
        }

        protected abstract Expression<Func<TSource, bool>> CreateCondition();

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

        public static bool operator true(Condition<TSource> condition)
        {
            return true;
        }

        public static bool operator false(Condition<TSource> condition)
        {
            return false;
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
    }

    public class BlankCondition<TSource> : Condition<TSource>
    {
        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            return _ => true;
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