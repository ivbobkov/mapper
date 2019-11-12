using NUnit.Framework;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;
using static TinyMapper.Tests.LiveSamples.ToolClasses;
using static TinyMapper.Tests.LiveSamples.ToolGroups;
using static TinyMapper.Tests.LiveSamples.ToolClassFields;

namespace TinyMapper.Tests
{
    public class FaceMillProfile : MappingProfile
    {
        public FaceMillProfile()
        {
            DefineMapping<FakeCatalog, FakeToolDto>()
                .ExecuteIf(Has.Parameter(M04) && Has.Group(G_04))
                .Include()
                .For(x => x.PointDiameter);
        }
    }

    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
        {
            var executionClause_1 = Has.Class(M04) && Has.Group(G_01);
                
                //HasClass(M04).And(HasGroup(G_01));
            var for_CornerRadius_1 = Has.Parameter(CornerRadius);

            //M04, G_04
            var executionClause_2 = Has.Parameter(M04) && (Has.Group(G_04) && Has.Parameters(DC));

            var for_PointDiameter_clause_1 = Has.Parameters(DC, DC2);
            var for_PointDiameter_clause_2 = Has.Parameters(DC, A_KAPPA);
            var for_PointDiameter_clause_3 = Absence.Parameter(DC2) && Has.Value(A_KAPPA, 90);
        }
    }
}