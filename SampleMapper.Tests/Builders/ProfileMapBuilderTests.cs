using System;
using System.Linq;
using NUnit.Framework;
using SampleMapper.Builders;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests.Builders
{
    [TestFixture]
    public class ProfileMapBuilderTests
    {
        private IProfileMapBuilder<FakeSource, FakeReceiver> _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = CreateSubject();
        }

        [Test]
        public void UseExecutionClause_NullReceived_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _subject.UseExecutionClause(null));
        }

        [Test]
        public void UseExecutionClause_UseAsDefaultIsAlreadySet_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _subject
                .UseAsDefault()
                .UseExecutionClause(new BlankCondition<FakeSource>()));

            Assert.AreEqual("You could not use execution clause for default profile", exception.Message);
        }

        [Test]
        public void UseAsDefaultProfile_ExecutionClauseIsAlreadySet_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _subject
                .UseExecutionClause(new BlankCondition<FakeSource>())
                .UseAsDefault());

            Assert.AreEqual("Profile with execution clause could not be default", exception.Message);
        }

        [Test]
        public void Build_ExecutionClauseIsNotSetForNonDefaultProfile_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _subject.Build());

            Assert.AreEqual("Execution clause must be set for non default profile", exception.Message);
        }

        [Test]
        public void Build_NoPropertyMapsConfigured_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _subject
                .UseAsDefault()
                .Build());

            Assert.AreEqual("No property maps configured", exception.Message);
        }

        [Test]
        public void Build_ClassWithoutDefaultConstructor_Throws()
        {
            var subject = new ProfileMapBuilder<FakeSource, ClassWithoutDefaultConstructor>();

            var exception = Assert.Throws<ArgumentException>(() => subject
                .UseAsDefault()
                .For(x => x.Property, x => x.Do(new BlankResolver<FakeSource, string>()))
                .Build());

            Assert.AreEqual($"Default constructor for {nameof(ClassWithoutDefaultConstructor)} not found", exception.Message);
        }

        [Test]
        public void For_ThereIsAlreadyAddedPropertyMap_ReplaceByNewOne()
        {
            var profileMap = _subject
                .UseAsDefault()
                .For(x => x.StringValue, x => x.Do(new BlankResolver<FakeSource, string>()))
                .For(x => x.StringValue, x => x.Do(new ExpressionResolver<FakeSource, string>(c => c.StringValue)))
                .Build();

            Assert.Multiple(() =>
            {
                var propertyMaps = profileMap.PropertyMaps.ToList();
                Assert.AreEqual(1, propertyMaps.Count);

                var mappingActions = propertyMaps[0].MappingActions.ToList();
                Assert.AreEqual(1, mappingActions.Count);

                var mappingAction = mappingActions[0];
                Assert.IsAssignableFrom<BlankCondition<FakeSource>>(mappingAction.ExecutionClause);
                Assert.IsAssignableFrom<ExpressionResolver<FakeSource, string>>(mappingAction.ValueResolver);
            });
        }

        private IProfileMapBuilder<FakeSource, FakeReceiver> CreateSubject()
        {
            return new ProfileMapBuilder<FakeSource, FakeReceiver>();
        }

        private class ClassWithoutDefaultConstructor
        {
            public string Property { get; set; }

            public ClassWithoutDefaultConstructor(int property)
            {
            }
        }
    }
}