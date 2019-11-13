using System;
using NUnit.Framework;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests
{
    [TestFixture]
    public class MapperTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = CreateSubject();
        }

        [Test]
        public void Mapper_MappingIsNotConfigured_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => _mapper.Map<FakeSource, FakeReceiver>(new FakeSource()));
        }

        [Test]
        public void Mapper_NoDefaultMapping_ThrowsInvalidOperationException()
        {
            var profile = new FakeProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseExecutionClause(new AlwaysFalseCondition<FakeSource>());

            _mapper.LoadProfile(profile);

            Assert.Throws<InvalidOperationException>(() => _mapper.Map<FakeSource, FakeReceiver>(new FakeSource()));
        }

        [Test]
        public void Mapper_ConfiguredType_VerifyMapping()
        {
            var profile = new FakeProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseAsDefaultProfile()
                .For(x => x.StringValue, x => x.Do(new ExpressionResolver<FakeSource, string>(c => c.StringValue)))
                .For(x => x.IntValue, x => x.Do(new ExpressionResolver<FakeSource, int>(c => c.IntValue)));

            _mapper.LoadProfile(profile);
            var source = new FakeSource
            {
                StringValue = "Source value",
                IntValue = 10
            };

            var receiver = _mapper.Map<FakeSource, FakeReceiver>(source);

            Assert.AreEqual(source.StringValue, receiver.StringValue);
            Assert.AreEqual(source.IntValue, receiver.IntValue);
        }

        private IMapper CreateSubject()
        {
            return new Mapper();
        }
    }
}