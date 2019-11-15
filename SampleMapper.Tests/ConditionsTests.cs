using System.Collections;
using NUnit.Framework;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests
{
    [TestFixture]
    public class ConditionsTests
    {
        [TestCaseSource(typeof(ConditionsTests), nameof(SameConditions))]
        public void Compare_TwoSameConditions(ICondition left, ICondition right)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(left, right);
                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            });
        }

        public static IEnumerable SameConditions
        {
            get
            {
                yield return new TestCaseData(
                    HasCode("C1") & HasCode("C2") & HasCode("C3"),
                    HasCode("C2") & HasCode("C3") & HasCode("C1")
                );

                yield return new TestCaseData(
                    HasCode("C1") | (HasCode("C2") & HasCode("C3")) | (HasCode("C4") | HasCode("C5")),
                    HasCode("C5") | HasCode("C1") | (HasCode("C2") & HasCode("C3")) | HasCode("C4")
                );

                yield return new TestCaseData(
                    (HasCode("C1") | HasCode("C2")) & HasCode("C3"),
                    HasCode("C3") & (HasCode("C2") | HasCode("C1"))
                );
            }
        }

        public static HasCodeCondition HasCode(string code)
        {
            return new HasCodeCondition(code);
        }
    }
}