using NUnit.Framework;
using TinyMapper.Core2;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    [TestFixture]
    public class Class2
    {
        [Test]
        public void Test()
        {
            var mapping = new Mapping<FakeCatalog, FakeToolDto>
            {
                MappingClause = new HasParameterClause(ToolClasses.M04).And(new HasParameterClause(ToolGroups.G_01))
            };

            mapping.MappingExpressions.Add(new MappingExpression<FakeCatalog, FakeToolDto>());

        }
    }
}