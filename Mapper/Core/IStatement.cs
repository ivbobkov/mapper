using System;

namespace TinyMapper.Core
{
    public interface IStatement<TSource, TReceiver, TStatement>
        : IEquatable<TStatement>
        where TStatement : IStatement<TSource, TReceiver, TStatement>
    {
        Func<TSource, TReceiver> Compile();
    }
}