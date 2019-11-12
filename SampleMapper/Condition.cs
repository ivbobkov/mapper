using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public abstract class Condition<TSource> : ICondition
    {
        public LambdaExpression ToExpression() => CreateCondition();

        public abstract Expression<Func<TSource, bool>> CreateCondition();

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
}