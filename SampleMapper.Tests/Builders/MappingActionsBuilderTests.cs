using System;
using System.Linq;
using NUnit.Framework;
using SampleMapper.Builders;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests.Builders
{
    [TestFixture]
    public class MappingActionsBuilderTests
    {
        private IMappingActionsBuilder<FakeSource, string> _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new MappingActionsBuilder<FakeSource, string>();
        }

        [Test]
        public void Do_ResolverIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.Do(null));
        }

        [Test]
        public void Do_AlreadyAdded_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.Do(NoAction());
                _subject.Do(NoAction());
            });

            Assert.AreEqual("There is execution clause duplicate", exception.Message);
        }

        [Test]
        public void If_ConditionIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.If(null));
        }

        [Test]
        public void If_PreviousConditionIsNotConfigured_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC"));
                _subject.If(HasCode("DC2")).ThenDo(NoAction());
            });

            Assert.AreEqual("Last mapping action is not configured", exception.Message);
        }

        [Test]
        public void If_DuplicatedConditionIsAdded_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(NoAction());
                _subject.If(HasCode("DC")).ThenDo(NoAction());
            });

            Assert.AreEqual("There is execution clause duplicate", exception.Message);
        }

        [Test]
        public void IfThenDo_ResolverIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(null);
            });
        }

        [Test]
        public void ThenDo_NoActionsWereConfigured_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                var casted = (ICanAddThenDo<FakeSource, string>) _subject;
                casted.ThenDo(NoAction());
            });

            Assert.AreEqual("No mapping actions were configured", exception.Message);
        }

        [Test]
        public void IfThenDo_IfIsNotConfigured_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(NoAction());
                var casted = (ICanAddThenDo<FakeSource, string>) _subject;
                casted.ThenDo(NoAction());
            });

            Assert.AreEqual("Last mapping action was configured", exception.Message);
        }

        [Test]
        public void Build_NoConfiguredActions_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.Build().ToArray();
            });

            Assert.AreEqual("No mapping actions configured", exception.Message);
        }

        [Test]
        public void Build_ConditionalActionsConfiguredButDefaultDidNot_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(NoAction());
                _subject.Build().ToArray();
            });

            Assert.AreEqual("Conditional actions configured, but default at the end is not", exception.Message);
        }

        [Test]
        public void Build_ActionsConfiguredWell_VerifyOrder()
        {
            _subject.If(HasCode("Code")).ThenDo(NoAction());
            _subject.Do(NoAction());

            var mappingActions = _subject.Build().ToList();

            Assert.IsAssignableFrom<HasCodeCondition>(mappingActions[0].ExecutionClause);
            Assert.IsAssignableFrom<BlankResolver<FakeSource, string>>(mappingActions[0].ValueResolver);

            Assert.IsAssignableFrom<BlankCondition<FakeSource>>(mappingActions[1].ExecutionClause);
            Assert.IsAssignableFrom<BlankResolver<FakeSource, string>>(mappingActions[1].ValueResolver);
        }

        private static Condition<FakeSource> HasCode(string code)
        {
            return new HasCodeCondition(code);
        }

        private static BlankResolver<FakeSource, string> NoAction()
        {
            return new BlankResolver<FakeSource, string>();
        }
    }
}