using System;
using NUnit.Framework;
using SampleMapper.Tests.Mapper.Fakes;

namespace SampleMapper.Tests.Mapper
{
    [TestFixture]
    public class MapperTests
    {
        private IMapper _mapper;
        private IMapperConfiguration _mapperConfiguration;

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

            _mapperConfiguration.LoadProfile(profile);

            Assert.Throws<InvalidOperationException>(() => _mapper.Map<FakeSource, FakeReceiver>(CreateSource()));
        }

        [Test]
        public void Mapper_DirectMappingIsConfigured_VerifyMapping()
        {
            var profile = new FakeProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseAsDefaultProfile()
                .For(x => x.StringValue, x => x.Do(new ExpressionResolver<FakeSource, string>(c => c.StringValue)))
                .For(x => x.IntValue, x => x.Do(new ExpressionResolver<FakeSource, int>(c => c.IntValue)));

            _mapperConfiguration.LoadProfile(profile);
            var source = CreateSource();

            var receiver = _mapper.Map<FakeSource, FakeReceiver>(source);

            Assert.AreEqual(source.StringValue, receiver.StringValue);
            Assert.AreEqual(source.IntValue, receiver.IntValue);
        }

        private FakeSource CreateSource(string stringValue = "FakeValue", int intValue = 25)
        {
            return new FakeSource
            {
                StringValue = stringValue,
                IntValue = intValue
            };
        }

        private IMapper CreateSubject()
        {
            _mapperConfiguration = new MapperConfiguration();

            return new SampleMapper.Mapper(_mapperConfiguration);
        }
    }
}