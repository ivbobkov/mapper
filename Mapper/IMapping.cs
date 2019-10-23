using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TinyMapper
{
    public interface IMapping<TSource, TReceiver>
    {
        Expression<Func<TSource, bool>> ExecutionClause { get; }
        IEnumerable<IAction<TSource, TReceiver>> Actions { get; }
        Action<TSource, TReceiver> AfterMap { get; }
    }
}