using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using SampleMapper.Builders;
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

        [Test]
        public void Mapper_DirectMappingIsConfigured_VerifyMapping()
        {
            var profile = new MappingProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseAsDefault()
                .For(x => x.StringValue, x => x.Do(ExpressionResolve(c => c.StringValue)))
                .For(x => x.IntValue, x => x.Do(ExpressionResolve(c => c.IntValue)));

            _mapperConfiguration.LoadProfile(profile);
            var source = CreateSource();

            var receiver = _subject.Map<FakeSource, FakeReceiver>(source);

            Assert.AreEqual(source.StringValue, receiver.StringValue);
            Assert.AreEqual(source.IntValue, receiver.IntValue);
        }

        [Test]
        public void Mapper_ConditionMappingIsConfigured_FinishOnFirstHit()
        {
            var profile = new MappingProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseAsDefault()
                .For(x => x.StringValue, x =>
                {
                    x.If(HasCode("D")).ThenDo(ExpressionResolve(c => "No hit: never"));
                    x.If(HasCode("D1")).ThenDo(ExpressionResolve(c => "First hit: should"));
                    x.If(HasCode("D1") | HasCode("D2")).ThenDo(ExpressionResolve(c => "Second hit: never"));
                    x.Do(ExpressionResolve(c => "Default hit: never"));
                })
                .For(x => x.IntValue, x =>
                {
                    x.If(HasCode("X")).ThenDo(ExpressionResolve(c => -1));
                    x.If(HasCode("XF")).ThenDo(ExpressionResolve(c => -2));
                    x.If(HasCode("D2")).ThenDo(ExpressionResolve(c => 10));
                    x.If(HasCode("D1") | HasCode("D2")).ThenDo(ExpressionResolve(c => -3));
                    x.Do(ExpressionResolve(c => -4));
                });

            _mapperConfiguration.LoadProfile(profile);
            var source = CreateSource(codes: new[] { "D1", "D2" });

            var receiver = _subject.Map<FakeSource, FakeReceiver>(source);

            Assert.AreEqual("First hit: should", receiver.StringValue);
            Assert.AreEqual(10, receiver.IntValue);
        }

        [Test]
        public void Map_UsedPropertyMaps_CheckOverloading()
        {
            var propertyMaps = PropertyMapsBuilder<FakeSource, FakeReceiver>.Create()
                .For(x => x.IntValue, x => x.Do(ExpressionResolve(c => 10)))
                .For(x => x.StringValue, x => x.Do(ExpressionResolve(c => "VAL")))
                .Build();

            var profile = new MappingProfile();
            profile.CreateProfile<FakeSource, FakeReceiver>()
                .UseAsDefault()
                .UsePropertyMaps(propertyMaps)
                .For(x => x.IntValue, x => x.Do(ExpressionResolve(c => -5)));

            _mapperConfiguration.LoadProfile(profile);

            var receiver = _subject.Map<FakeSource, FakeReceiver>(CreateSource());

            Assert.AreEqual("VAL", receiver.StringValue);
            Assert.AreEqual(-5, receiver.IntValue);
        }

        private ExpressionResolver<FakeSource, TReceiverMember> ExpressionResolve<TReceiverMember>(Expression<Func<FakeSource, TReceiverMember>> body)
        {
            return new ExpressionResolver<FakeSource, TReceiverMember>(body);
        }

        private FakeSource CreateSource(
            string stringValue = "FakeValue",
            int intValue = 25,
            params string[] codes)
        {
            return new FakeSource
            {
                StringValue = stringValue,
                IntValue = intValue,
                Items = codes.Select(x => new FakeSourceItem
                {
                    Code = x
                })
            };
        }

        private IMapper CreateSubject()
        {
            _mapperConfiguration = new MapperConfiguration();

            return new Mapper(_mapperConfiguration);
        }

        private HasCodeCondition HasCode(string code)
        {
            return new HasCodeCondition(code);
        }
    }
}