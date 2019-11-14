using System.Collections.Generic;
using NUnit.Framework;
using SampleMapper.Tests.Mapper.Fakes;

namespace SampleMapper.Tests.Mapper
{
    [TestFixture]
    public class ConditionsTests
    {
        [TestCaseSource(nameof(GetSameConditions))]
        public void Compare_TwoSameConditions(ICondition left, ICondition right)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
                Assert.AreEqual(left, right);
            });
        }

        public static IEnumerable<TestCaseData> GetSameConditions()
        {
            yield return new TestCaseData(
                HasCode("C1") & HasCode("C2") & HasCode("C3"),
                HasCode("C2") & HasCode("C3") & HasCode("C1")
            );

            yield return new TestCaseData(
                (HasCode("C1") | HasCode("C2")) & HasCode("C3"),
                HasCode("C3") & (HasCode("C2") | HasCode("C1"))
            );

            yield return new TestCaseData(
                HasCode("C1") | (HasCode("C2") & HasCode("C3")) | (HasCode("C4") | HasCode("C5")),
                HasCode("C5") | HasCode("C1") | (HasCode("C2") & HasCode("C3")) | HasCode("C4")
            );
        }

        public static HasCodeCondition HasCode(string code)
        {
            return new HasCodeCondition(code);
        }
    }
}