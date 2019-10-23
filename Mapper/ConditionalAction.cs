using System;
using System.Linq.Expressions;

namespace TinyMapper
{
    public class ConditionalAction<TSource, TReceiver> : IAction<TSource, TReceiver>
    {
        private readonly Func<TSource, bool> _clause;

        public Expression<Func<TSource, bool>> Clause { get; }
        public IAction<TSource, TReceiver> IfStatement { get; private set; }
        public IAction<TSource, TReceiver> ElseStatement { get; private set; }

        public ConditionalAction(Expression<Func<TSource, bool>> clause)
        {
            _clause = clause.Compile();
            Clause = clause;
        }

        public ConditionalAction(
            Expression<Func<TSource, bool>> clause,
            IAction<TSource, TReceiver> ifStatement,
            IAction<TSource, TReceiver> elseStatement)
            : this(clause)
        {
            IfStatement = ifStatement;
            ElseStatement = elseStatement;
        }

        public TReceiver Execute(TReceiver receiver, TSource source)
        {
            return _clause(source)
                ? IfStatement.Execute(receiver, source)
                : ElseStatement.Execute(receiver, source);
        }

        internal void AddIfStatement(IAction<TSource, TReceiver> ifAction)
        {
            IfStatement = ifAction;
        }

        internal void AddElseStatement(IAction<TSource, TReceiver> elseAction)
        {
            ElseStatement = elseAction;
        }
    }
}