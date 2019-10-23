using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TinyMapper
{
    public class Mapping<TSource, TReceiver> : IMapping<TSource, TReceiver>
    {
        public Mapping(
            Expression<Func<TSource, bool>> executionClause,
            IEnumerable<IAction<TSource, TReceiver>> actions,
            Action<TSource, TReceiver> afterMap)
        {
            ExecutionClause = executionClause;
            Actions = actions;
            AfterMap = afterMap;
        }

        public Expression<Func<TSource, bool>> ExecutionClause { get; }
        public IEnumerable<IAction<TSource, TReceiver>> Actions { get; }
        public Action<TSource, TReceiver> AfterMap { get; }
    }
}