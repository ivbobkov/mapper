using System;
using System.Linq;
using System.Linq.Expressions;
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
        public void Do_ResolverIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.Do(null));
        }

        [Test]
        public void Do_AlreadyAdded_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.Do(Resolve(c => c.StringValue));
                _subject.Do(Resolve(c => ""));
            }, "There is execution clause duplicate");
        }

        [Test]
        public void If_ConditionIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.If(null), "");
        }

        [Test]
        public void If_PreviousConditionIsNotConfigured_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC"));
                _subject.If(HasCode("DC2")).ThenDo(Resolve(x => "OK"));
            }, "Last mapping action is not configured");
        }

        [Test]
        public void If_DuplicatedConditionIsAdded_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(Resolve(x => "OK"));
                _subject.If(HasCode("DC")).ThenDo(Resolve(x => "Exception"));
            }, "There is execution clause duplicate");
        }

        [Test]
        public void IfThenDo_ResolverIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(null);
            });
        }

        [Test]
        public void ThenDo_NoActionsWereConfigured_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var casted = (ICanAddThenDo<FakeSource, string>) _subject;
                casted.ThenDo(Resolve(x => "OK"));
            }, "No mapping actions were configured");
        }

        [Test]
        public void IfThenDo_IfIsNotConfigured_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(Resolve(x => "OK"));
                var casted = (ICanAddThenDo<FakeSource, string>) _subject;
                casted.ThenDo(Resolve(x => "NEW"));
            }, "Last mapping action was configured");
        }

        [Test]
        public void Build_NoActionsConfigured_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.Build().ToArray();
            }, "No mapping actions configured");
        }

        [Test]
        public void Build_ConditionalActionsConfiguredNoDefaultActionConfigured_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _subject.If(HasCode("DC")).ThenDo(Resolve(x => "OK"));
                _subject.Build().ToArray();
            }, "Conditional actions configured, but default at the end is not");
        }

        [Test]
        public void Build_ActionsConfiguredWell_VerifyOrder()
        {
            _subject.If(HasCode("Code")).ThenDo(Resolve(x => "DO"));
            _subject.Do(Resolve(x => "Default"));

            var mappingActions = _subject.Build().ToList();

            Assert.IsAssignableFrom<HasCodeCondition>(mappingActions[0].ExecutionClause);
            Assert.IsAssignableFrom<ExpressionResolver<FakeSource, string>>(mappingActions[0].ValueResolver);

            Assert.IsAssignableFrom<BlankCondition<FakeSource>>(mappingActions[1].ExecutionClause);
            Assert.IsAssignableFrom<ExpressionResolver<FakeSource, string>>(mappingActions[1].ValueResolver);
        }

        private static Condition<FakeSource> HasCode(string code)
        {
            return new HasCodeCondition(code);
        }

        private static ValueResolver<FakeSource, TReceiverMember> Resolve<TReceiverMember>(Expression<Func<FakeSource, TReceiverMember>> resolver)
        {
            return new ExpressionResolver<FakeSource, TReceiverMember>(resolver);
        }
    }
}