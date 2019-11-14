using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper.Tests.Mapper.Fakes
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
            const int deviation = 5; // any random number
            var stateHash = Code.GetHashCode();
            var typeHash = GetType().GetHashCode();

            return ((stateHash << deviation) + stateHash) ^ typeHash;
        }

        public static HasCodeCondition Create(string code)
        {
            return new HasCodeCondition(code);
        }
    }
}