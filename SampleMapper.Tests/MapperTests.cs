using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using SampleMapper.Tests.Fakes;

namespace SampleMapper.Tests
{
    [TestFixture]
    public class MapperTests
    {
        private IMapper _subject;
        private IMapperConfiguration _mapperConfiguration;

        [SetUp]
        public void SetUp()
        {
            _subject = CreateSubject();
        }


        //[Test]
        //public void Mapper_MappingIsNotConfigured_ThrowsInvalidOperationException()
        //{
        //    Assert.Throws<InvalidOperationException>(() => _subject.Map<FakeSource, FakeReceiver>(new FakeSource()));
        //}

        //[Test]
        //public void Mapper_NoDefaultMapping_ThrowsInvalidOperationException()
        //{
        //    var profile = new FakeProfile();
        //    profile.CreateProfile<FakeSource, FakeReceiver>()
        //        .UseExecutionClause(new AlwaysFalseCondition<FakeSource>());

        //    _mapperConfiguration.LoadProfile(profile);

        //    Assert.Throws<InvalidOperationException>(() => _subject.Map<FakeSource, FakeReceiver>(CreateSource()));
        //}

        [Test]
        public void Mapper_DirectMappingIsConfigured_VerifyMapping()
        {
            var profile = new MappingProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseAsDefaultProfile()
                .For(x => x.StringValue, x => x.Do(CreateClause(c => c.StringValue)))
                .For(x => x.IntValue, x => x.Do(CreateClause(c => c.IntValue)));

            _mapperConfiguration.LoadProfile(profile);
            var source = CreateSource();

            var receiver = _subject.Map<FakeSource, FakeReceiver>(source);

            Assert.AreEqual(source.StringValue, receiver.StringValue);
            Assert.AreEqual(source.IntValue, receiver.IntValue);
        }

        private ExpressionResolver<FakeSource, TReceiverMember> CreateClause<TReceiverMember>(Expression<Func<FakeSource, TReceiverMember>> body)
        {
            return new ExpressionResolver<FakeSource, TReceiverMember>(body);
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