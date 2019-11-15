using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests
{
    [TestFixture]
    public class ConditionsTests
    {
        [TestCaseSource(typeof(ConditionsTests), nameof(CompareTestData))]
        public void Compare_TwoSameConditions(ICondition left, ICondition right)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(left, right);
                Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            });
        }

        [TestCaseSource(typeof(ConditionsTests), nameof(CheckExecutionTestData))]
        public bool CheckLambdaExecution(ICondition condition, FakeSource source)
        {
            var lambda = condition.AsLambda();
            var func = (Func<FakeSource, bool>)lambda.Compile();

            return func(source);
        }

        [TestCaseSource(typeof(ConditionsTests), nameof(CheckExecutionTestData))]
        public bool CheckIsMatchExecution(ICondition condition, FakeSource source)
        {
            var castedCondition = (Condition<FakeSource>) condition;

            return castedCondition.IsMatch(source);
        }

        public static IEnumerable CompareTestData
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

        public static IEnumerable CheckExecutionTestData
        {
            get
            {
                yield return new TestCaseData(
                    HasCode("C1"),
                    CreateSource("C1", "C3", "C2")
                ).Returns(true);
                
                yield return new TestCaseData(
                    HasCode("C1") & HasCode("C2"),
                    CreateSource("C1", "C3", "C2")
                ).Returns(true);

                yield return new TestCaseData(
                    HasCode("C1") & HasCode("C2") & HasCode("C3"),
                    CreateSource("C1", "C2")
                ).Returns(false);

                yield return new TestCaseData(
                    HasCode("C1") | HasCode("C2"),
                    CreateSource("C1")
                ).Returns(true);

                yield return new TestCaseData(
                    HasCode("C1") | HasCode("C2"),
                    CreateSource("C1")
                ).Returns(true);

                yield return new TestCaseData(
                    HasCode("C1") | HasCode("C2"),
                    CreateSource("C3")
                ).Returns(false);

                yield return new TestCaseData(
                    HasCode("C1") & !HasCode("C2"),
                    CreateSource("C1", "C2")
                ).Returns(false);

                yield return new TestCaseData(
                    !HasCode("C1") | !HasCode("C2"),
                    CreateSource("C3")
                ).Returns(false);
            }
        }

        public static FakeSource CreateSource(params string[] codes)
        {
            return new FakeSource
            {
                Items = codes.Select(code => new FakeSourceItem
                {
                    Code = code
                })
            };
        }

        public static HasCodeCondition HasCode(string code)
        {
            return new HasCodeCondition(code);
        }
    }
}