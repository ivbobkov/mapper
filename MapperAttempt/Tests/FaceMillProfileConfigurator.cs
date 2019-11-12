using NUnit.Framework;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests.Tests
{
    //public class FaceMillProfileConfigurator : ProfileConfigurator
    //{
    //    public FaceMillProfileConfigurator()
    //    {
    //        DefineMapping<FakeCatalog, FakeToolDto>()
    //            .ExecuteIf(Has.Parameter(ToolClasses.M04) && Has.Group(ToolGroups.G_04))
    //            .Include()
    //            .For(x => x.PointDiameter);
    //    }
    //}

    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test()
        {
            var executionClause_1 = Has.Class(ToolClasses.M04) && Has.Group(ToolGroups.G_01);
                
                //HasClass(M04).And(HasGroup(G_01));
            var for_CornerRadius_1 = Has.Parameter(ToolClassFields.CornerRadius);

            //M04, G_04
            var executionClause_2 = Has.Parameter(ToolClasses.M04) && (Has.Group(ToolGroups.G_04) && Has.Parameters(ToolClassFields.DC));

            var for_PointDiameter_clause_1 = Has.Parameters(ToolClassFields.DC, ToolClassFields.DC2);
            var for_PointDiameter_clause_2 = Has.Parameters(ToolClassFields.DC, ToolClassFields.A_KAPPA);
            var for_PointDiameter_clause_3 = Absence.Parameter(ToolClassFields.DC2) && Has.Value(ToolClassFields.A_KAPPA, 90);
        }
    }
}