using System;
using NUnit.Framework;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests
{
    [TestFixture]
    public class MappingProfileTests
    {
        private MappingProfile _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new MappingProfile();
        }

        [Test]
        public void BuildProfileMaps_DuplicatedProfileMap_Throws()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                _subject
                    .CreateProfile<FakeSource, FakeReceiver>()
                    .UseAsDefault()
                    .For(x => x.StringValue, x => x.Do(NoAction<string>()));

                _subject
                    .CreateProfile<FakeSource, FakeReceiver>()
                    .UseAsDefault()
                    .For(x => x.IntValue, x => x.Do(NoAction<int>()));

                _subject.BuildProfileMaps();
            });

            Assert.AreEqual(
                "There is duplicate for: <SampleMapper.Tests.Fakes.FakeSource, SampleMapper.Tests.Fakes.FakeReceiver> IsDefault True",
                exception.Message);
        }

        public static BlankCondition<FakeSource> BlankCondition()
        {
            return new BlankCondition<FakeSource>();
        }

        public static BlankResolver<FakeSource, TReceiverMember> NoAction<TReceiverMember>()
        {
            return new BlankResolver<FakeSource, TReceiverMember>();
        }
    }
}