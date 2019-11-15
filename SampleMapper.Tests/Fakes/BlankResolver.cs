using System;
using System.Linq.Expressions;

namespace SampleMapper.Tests.Fakes
{
    public class BlankResolver<TSource, TReceiverMember> : ValueResolver<TSource, TReceiverMember>
    {
        protected override Expression<Func<TSource, TReceiverMember>> CreateResolver()
        {
            return _ => default(TReceiverMember);
        }
    }
}