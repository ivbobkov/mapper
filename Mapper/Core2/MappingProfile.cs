using System;
using System.Linq.Expressions;

namespace TinyMapper.Core2
{
    public abstract class MappingProfile
    {
        protected IMappingBuilder<TSource, TReceiver> DefineMapping<TSource, TReceiver>()
        {
            return null;
        }
    }

    public interface IMappingBuilder<TSource, TReceiver>
    {
        IMappingBuilder<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> receiverMember);
        IMappingBuilder<TSource, TReceiver> Include();
        IMappingBuilder<TSource, TReceiver> ExecuteIf(Clause<TSource> clause);
    }
}