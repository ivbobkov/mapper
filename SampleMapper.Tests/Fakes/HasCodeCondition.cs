using System;
using System.Linq;
using System.Linq.Expressions;
using SampleMapper.Helpers;

namespace SampleMapper.Tests.Fakes
{
    public class HasCodeCondition : Condition<FakeSource>
    {
        public HasCodeCondition(string code)
        {
            Code = code;
        }

        public string Code { get; }

        protected override Expression<Func<FakeSource, bool>> CreateCondition()
        {
            return source => source.Items.Any(c => c.Code.Equals(Code));
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.ResolveHashForType(Code.GetHashCode(), GetType());
        }
    }
}