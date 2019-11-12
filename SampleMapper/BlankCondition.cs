using System;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class BlankCondition<TSource> : Condition<TSource>
    {
        public override Expression<Func<TSource, bool>> CreateCondition()
        {
            return _ => true;
        }
    }
}