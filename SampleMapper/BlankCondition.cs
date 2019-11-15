using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class BlankCondition<TSource> : Condition<TSource>
    {
        protected override Expression<Func<TSource, bool>> CreateCondition()
        {
            return _ => true;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}