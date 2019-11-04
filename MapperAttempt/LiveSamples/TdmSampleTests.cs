using System;
using System.Linq.Expressions;
using NUnit.Framework;
using TinyMapper.Tests.LiveSamples.Test;
using static TinyMapper.Tests.LiveSamples.ToolClassFields;
using static TinyMapper.Tests.LiveSamples.ToolClasses;
using static TinyMapper.Tests.LiveSamples.ToolGroups;

namespace TinyMapper.Tests.LiveSamples
{
    public class FaceMillMappingProfile : MappingProfileBase
    {
        public FaceMillMappingProfile()
        {
            DefineMapping<FakeCatalog, FakeToolDto>()
                .ExecuteIf(new ClassGroupClause(M04, G_01))
                //.ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_01))
                .Include()
                .For(x => x.PointDiameter, a => a.Assign(0))
                .For(x => x.PointAngle, a => a.Assign(c => c.Double(A_KAPPA), Angles.SettingToSigma))
                .For(x => x.CornerRadius, a => a
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0));

            DefineMapping<FakeCatalog, FakeToolDto>()
                .ExecuteIf(new ClassGroupClause(M04, G_03))
                //.ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_03))
                .Include()
                .For(x => x.PointDiameter, x => x.Assign(0))
                .For(x => x.PointAngle, x => x.Assign(c => c.Double(A_KAPPA), Angles.SettingToSigma))
                .For(x => x.CornerRadius, x => x.Assign(0));

            DefineMapping<FakeCatalog, FakeToolDto>()
                .ExecuteIf(new ClassGroupClause(M04, G_04))
                //.ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_04))
                .Include()
                .For(x => x.PointDiameter, a => a
                    .If(x => x.Has(DC, DC2))
                    .Assign(c => c.Double(DC2))
                    .Else.If(x => x.Has(DC, A_KAPPA))
                    .Assign(c => /* Math.Atan(c.Double("DC2")) ... */ 0)
                    .Else.If(x => x.Absence(DC2) && x.Double(A_KAPPA) == 90).Assign(0)
                    .Else.Assign(default(double?))
                )
                .For(x => x.PointAngle, a => a.Assign(c => c.Double(A_KAPPA), Angles.SettingToSigma))
                .For(x => x.CornerRadius, action => action
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0)
                );

            DefineMapping<FakeCatalog, FakeToolDto>()
                .ExecuteIf(new ClassGroupClause(M04, G_04))
                .Include()
                .For(x => x.PointDiameter, a => a.Assign(0))
                .For(x => x.PointAngle, a => a.Assign(c => c.Double(A_KAPPA), Angles.SettingToSigma))
                .For(x => x.CornerRadius, a => a
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0)
                );
        }
    }

    public class EndMillMappingProfile : MappingProfileBase
    {
        public EndMillMappingProfile()
        {
        }
    }

    [TestFixture]
    public class TdmSampleTests
    {
        private static IMapper Mapper<TSource, TReceiver>()
        {
            var mappingProvider = new MappingProvider();
            mappingProvider.RegisterProfile(new FaceMillMappingProfile());
            mappingProvider.RegisterProfile(new EndMillMappingProfile());

            return new Mapper(mappingProvider);
        }

        [Test]
        public void TestCase()
        {
            var fakeCatalog = new FakeCatalog();

            var toolDto = Mapper<FakeCatalog, FakeToolDto>()
                .Map<FakeCatalog, FakeToolDto>(fakeCatalog);
        }

        // унести в другой класс
        private IAction<FakeCatalog, FakeToolDto>[] CommonCutterActions
        {
            get
            {
                throw new NotImplementedException();
                //var actionBuilders = new ActionsBuilder<FakeCatalog, FakeToolDto>();

                //actionBuilders
                //    .For(x => x.Id, x => x.Assign(c => c.CatalogId))
                //    .For(x => x.ArticleNumber, x => x.Assign(c => c.CatalogId))
                //    .For(x => x.BodyDiameter, x => x.Assign(c => c.Double(BodyDiameter)))
                //    .For(x => x.HasCooling, x => x.Assign(c => c.Boolean(Coolant)))
                //    .For(x => x.DC, x => x.Assign(c => c.Double(DC)))
                //    .For(x => x.CuttingEdgeLength, x => x.Assign(c => c.Double(CuttingEdgeLength)))
                //    .For(x => x.GaugeLength, x => x.Assign(c => c.Double(GaugeLength)))
                //    .For(x => x.HelixAngle, x => x.Assign(c => c.Double(HelixAngle)))
                //    .For(x => x.ShankDiameter, x => x.Assign(c => c.Double(ShankDiameter)))
                //    .For(x => x.TotalLength, x => x.Assign(c => c.Double(ShankDiameter)))
                //    .For(x => x.EdgeNumber, x => x.Assign(c => c.Double(CuttingEdgeCount)));

                //return actionBuilders.Compile();
            }
        }
    }

    public static class Clauses
    {
        public static Expression<Func<FakeCatalog, bool>> ByClassAndGroup(string @class, string @group)
        {
            Expression<Func<FakeCatalog, bool>> clause = input =>
                input.ToolClassId.Equals(@class)
                && input.ToolGroupId.Equals(@group);

            return clause;
        }
    }

    public static class Angles
    {
        public static readonly Expression<Func<double?, double?>> SettingToSigma = (input) => input.HasValue ? 180 - input * 2 : input;
    }
}