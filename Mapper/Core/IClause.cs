using System;

namespace TinyMapper.Core
{
    public interface IClause<TSource, TClause> :
        IEquatable<TClause>
        where TClause : IClause<TSource, TClause>
    {
        Func<TSource, bool> Compile();
    }
}