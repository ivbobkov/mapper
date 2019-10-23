using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using TinyMapper.Builders;
using static TinyMapper.Tests.LiveSamples.ToolClassFields;
using static TinyMapper.Tests.LiveSamples.ToolClasses;
using static TinyMapper.Tests.LiveSamples.ToolGroups;

namespace TinyMapper.Tests.LiveSamples
{
    [TestFixture]
    public class TdmSampleTests
    {
        [Test]
        public void TestCase()
        {
            DefineMap<FakeCatalog, FakeToolDto>()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_01))
                .Include(CommonCutterActions)
                .ActionFor(x => x.PointDiameter, a => a.Assign(0))
                .ActionFor(x => x.PointAngle, a => a.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .ActionFor(x => x.CornerRadius, x => x
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0)
                );

            DefineMap<FakeCatalog, FakeToolDto>()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_03))
                .Include(CommonCutterActions)
                .ActionFor(x => x.PointDiameter, x => x.Assign(0))
                .ActionFor(x => x.PointAngle, x => x.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .ActionFor(x => x.CornerRadius, x => x.Assign(0));

            DefineMap<FakeCatalog, FakeToolDto>()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_04))
                .Include(CommonCutterActions)
                .ActionFor(x => x.PointDiameter, b => b
                    .If(x => x.Has(CuttingEdgeDiameter, CuttingEdgeDiameter2)).Assign(c => c.Double(CuttingEdgeDiameter2))
                    .Else.If(x => x.Has(CuttingEdgeDiameter, SettingAngle)).Assign(c => /* Math.Atan(c.Double("DC2")) ... */ 0)
                    .Else.If(x => x.Absence(CuttingEdgeDiameter2) && x.Double(SettingAngle) == 90).Assign(0)
                    .Else.Assign(default(double?))
                )
                .ActionFor(x => x.PointAngle,
                    action => action.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .ActionFor(x => x.CornerRadius, action => action
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0));

            DefineMap<FakeCatalog, FakeToolDto>()
                .ExecuteIf(x => x.ToolClassId.Equals(M04) && x.ToolGroupId.Equals(G_05))
                .Include(CommonCutterActions)
                .ActionFor(x => x.PointDiameter, action => action.Assign(0))
                .ActionFor(x => x.PointAngle, action => action.Assign(c => c.Double(SettingAngle), Angles.SettingToSigma))
                .ActionFor(x => x.CornerRadius, action => action
                    .If(c => c.Has(CornerRadius)).Assign(c => c.Double(CornerRadius))
                    .Else.Assign(0)
                );
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

        private IMappingBuilder<TSource, TReceiver> DefineMap<TSource, TReceiver>()
        {
            return new MappingBuilder<TSource, TReceiver>();
        }
    }

    public static class Angles
    {

        public static readonly Expression<Func<double?, double?>> SettingToSigma = (input) => input.HasValue ? 180 - input * 2 : input;
    }
}