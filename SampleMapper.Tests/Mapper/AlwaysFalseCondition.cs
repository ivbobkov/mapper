using System;
using System.Linq.Expressions;

namespace SampleMapper.Tests.Mapper
{
    public class AlwaysFalseCondition<TSource> : Condition<TSource>
    {
        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            return _ => false;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}