using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using TinyMapper.Builders;
using TinyMapper.Core;
using static TinyMapper.Tests.LiveSamples.ToolClassFields;
using static TinyMapper.Tests.LiveSamples.ToolClasses;
using static TinyMapper.Tests.LiveSamples.ToolGroups;

namespace TinyMapper.Tests.LiveSamples
{
    public class FaceMillMappingProfile : MappingProfileBase<FakeCatalog, FakeToolDto>
    {
        public FaceMillMappingProfile()
        {
            DefineMapping()
                .ExecuteIf(Clauses.ByClassAndGroup(M04, G_01))
                //.ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_01))
                .Include()
                .For(x => x.PointDiameter, a => a.Assign(0))
                .For(x => x.PointAngle, a => a.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .For(x => x.CornerRadius, a => a
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0));

            DefineMapping()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_03))
                .Include()
                .For(x => x.PointDiameter, x => x.Assign(0))
                .For(x => x.PointAngle, x => x.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .For(x => x.CornerRadius, x => x.Assign(0));

            DefineMapping()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_04))
                .Include()
                .For(x => x.PointDiameter, a => a
                    .If(x => x.Has(CuttingEdgeDiameter, CuttingEdgeDiameter2))
                    .Assign(c => c.Double(CuttingEdgeDiameter2))
                    .Else.If(x => x.Has(CuttingEdgeDiameter, SettingAngle))
                    .Assign(c => /* Math.Atan(c.Double("DC2")) ... */ 0)
                    .Else.If(x => x.Absence(CuttingEdgeDiameter2) && x.Double(SettingAngle) == 90).Assign(0)
                    .Else.Assign(default(double?))
                )
                .For(x => x.PointAngle, a => a.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .For(x => x.CornerRadius, action => action
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0)
                );

            DefineMapping()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_05))
                .Include()
                .For(x => x.PointDiameter, a => a.Assign(0))
                .For(x => x.PointAngle, a => a.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .For(x => x.CornerRadius, a => a
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0)
                );
        }
    }

    public class EndMillMappingProfile : MappingProfileBase<FakeCatalog, FakeToolDto>
    {
        public EndMillMappingProfile()
        {
        }
    }

    [TestFixture]
    public class TdmSampleTests
    {
        private static IMapper<FakeCatalog, FakeToolDto> Mapper
        {
            get
            {
                var mappingProvider = new MappingProvider<FakeCatalog, FakeToolDto>();
                mappingProvider.RegisterProfile(new FaceMillMappingProfile());
                mappingProvider.RegisterProfile(new EndMillMappingProfile());

                return new Mapper<FakeCatalog, FakeToolDto>(mappingProvider);
            }
        }

        [Test]
        public void TestCase()
        {
            var toolDto = Mapper.Map(new FakeCatalog { Name = "" });
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
                //    .For(x => x.CuttingEdgeDiameter, x => x.Assign(c => c.Double(CuttingEdgeDiameter)))
                //    .For(x => x.CuttingEdgeLength, x => x.Assign(c => c.Double(CuttingEdgeLength)))
                //    .For(x => x.GaugeLength, x => x.Assign(c => c.Double(GaugeLength)))
                //    .For(x => x.HelixAngle, x => x.Assign(c => c.Double(HelixAngle)))
                //    .For(x => x.ShankDiameter, x => x.Assign(c => c.Double(ShankDiameter)))
                //    .For(x => x.TotalLength, x => x.Assign(c => c.Double(ShankDiameter)))
                //    .For(x => x.EdgeNumber, x => x.Assign(c => c.Double(CuttingEdgeCount)));

                //return actionBuilders.Build();
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