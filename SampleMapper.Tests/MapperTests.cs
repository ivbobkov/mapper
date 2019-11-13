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
            var profile = new FakeDynamicProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseExecutionClause(new AlwaysFalseCondition<FakeSource>());

            _mapper.LoadProfile(profile);

            Assert.Throws<InvalidOperationException>(() => _mapper.Map<FakeSource, FakeReceiver>(new FakeSource()));
        }

        private IMapper CreateSubject()
        {
            return new Mapper();
        }
    }
}