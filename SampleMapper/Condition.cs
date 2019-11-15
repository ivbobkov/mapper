using System;
using System.Linq.Expressions;
using SampleMapper.Internal;

namespace SampleMapper
{
    public abstract class Condition<TSource> : ICondition
    {
        private readonly Lazy<Expression<Func<TSource, bool>>> _conditionExpression;
        private readonly Lazy<Func<TSource, bool>> _conditionFunc;

        protected Condition()
        {
            _conditionExpression = new Lazy<Expression<Func<TSource, bool>>>(() => CreateCondition());
            _conditionFunc = new Lazy<Func<TSource, bool>>(() => _conditionExpression.Value.Compile());
        }

        public LambdaExpression AsLambda() => _conditionExpression.Value;

        public bool IsMatch(TSource source)
        {
            return _conditionFunc.Value(source);
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

        public bool Equals(ICondition other)
        {
            if (other == null)
            {
                return false;
            }

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var condition = obj as ICondition;

            if (condition == null)
            {
                return false;
            }

            return Equals(condition);
        }

        public abstract override int GetHashCode();
    }
}