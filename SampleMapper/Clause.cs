using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper
{
    public static class ClauseExtensions
    {
        public static Clause<TSource> And<TSource>(this Clause<TSource> source, Clause<TSource> clause)
        {
            return new AndClause<TSource>(source, clause);
        }

        public static Clause<TSource> Or<TSource>(this Clause<TSource> source, Clause<TSource> clause)
        {
            return new OrClause<TSource>(source, clause);
        }

        public static Clause<TSource> Not<TSource>(this Clause<TSource> source)
        {
            return new NotClause<TSource>(source);
        }
    }

    public abstract class Clause<TSource>
    {
        public abstract Expression<Func<TSource, bool>> ToExpression();

        public static Clause<TSource> operator &(Clause<TSource> left, Clause<TSource> right)
        {
            return new AndClause<TSource>(left, right);
        }

        public static Clause<TSource> operator |(Clause<TSource> left, Clause<TSource> right)
        {
            return new OrClause<TSource>(left, right);
        }

        public static Clause<TSource> operator !(Clause<TSource> clause)
        {
            return new NotClause<TSource>(clause);
        }

        public static bool operator true(Clause<TSource> clause)
        {
            return true;
        }

        public static bool operator false(Clause<TSource> clause)
        {
            return false;
        }
    }

    public class AndClause<TSource> : Clause<TSource>
    {
        private readonly Clause<TSource> _left;
        private readonly Clause<TSource> _right;

        public AndClause(Clause<TSource> left, Clause<TSource> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TSource, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }

    public class OrClause<TSource> : Clause<TSource>
    {
        private readonly Clause<TSource> _left;
        private readonly Clause<TSource> _right;

        public OrClause(Clause<TSource> left, Clause<TSource> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TSource, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();

            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }

    public class NotClause<TSource> : Clause<TSource>
    {
        private readonly Clause<TSource> _clause;

        public NotClause(Clause<TSource> clause)
        {
            _clause = clause;
        }

        public override Expression<Func<TSource, bool>> ToExpression()
        {
            var clauseExpression = _clause.ToExpression();
            var parameter = clauseExpression.Parameters.Single();
            var body = Expression.Not(clauseExpression.Body);

            return Expression.Lambda<Func<TSource, bool>>(body, parameter);
        }
    }
}