using System.Linq.Expressions;
using NUnit.Framework;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;
using static  TinyMapper.Tests.LiveSamples.ToolClassFields;

namespace TinyMapper.Tests.Tests
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void RawView()
        {
            var mapping_M04_G_01 = new ProfileMap
            {
                TypePair = TypePair.Create<FakeCatalog, FakeToolDto>(),
                Condition = Has.Class(ToolClasses.M04) && Has.Group(ToolGroups.G_01),
                PropertyMaps =
                {
                    new PropertyMap
                    {
                        ReceiverMember = Expression.Parameter(typeof(double?), "PointDiameter"),
                        MemberMaps =
                        {
                            new MemberMap
                            {
                                Condition = new BlankCondition(),
                                ValueResolver = Resolve.Constant(0d)
                            }
                        }
                    },
                    new PropertyMap
                    {
                        ReceiverMember = Expression.Parameter(typeof(double?), "PointAngle"),
                        MemberMaps =
                        {
                            new MemberMap
                            {
                                Condition = new BlankCondition(),
                                ValueResolver = Resolve.PointFromSettingAngle("A KAPPA")
                            }
                        }
                    },
                    new PropertyMap
                    {
                        ReceiverMember = Expression.Parameter(typeof(double?), "CornerRadius"),
                        MemberMaps =
                        {
                            new MemberMap
                            {
                                Condition = Has.Parameter("R CORNER"),
                                ValueResolver = Resolve.DoubleFromParameter("R CORNER")
                            },
                            new MemberMap
                            {
                                Condition = new BlankCondition(),
                                ValueResolver = Resolve.Constant(0d)
                            }
                        }
                    }
                }
            };
        }

        

        public class FaceMillProfile : ProfileConfigurator
        {
            public FaceMillProfile()
            {
                Define<FakeCatalog, FakeToolDto>()
                    .Condition(Has.Class(ToolClasses.M04) && Has.Group(ToolGroups.G_01))
                    .Include(/* TO BE DONE */)
                    .For(x => x.PointDiameter, x => x.Do(Resolve.Constant(new double?(0))))
                    .For(x => x.PointAngle, x => x.Do(Resolve.PointFromSettingAngle(A_KAPPA)))
                    .For(x => x.CornerRadius, x =>
                    {
                        x.If(Has.Parameter(CornerRadius)).Do(Resolve.DoubleFromParameter(CornerRadius));
                        x.Do(Resolve.Constant(new double?(0)));
                    });

                Define<FakeCatalog, FakeToolDto>()
                    .Condition(Has.Class(ToolClasses.M04) && Has.Group(ToolGroups.G_04))
                    .Include(/* TO BE DONE */)
                    .For(x => x.PointDiameter, x =>
                    {
                        x.If(Has.Parameters(DC, DC2)).Do(Resolve.DoubleFromParameter(DC2));
                        x.If(Has.Parameters(DC, A_KAPPA)).Do(null /* Math.Atan(c.DoubleFromParameter("DC2")) ... */);
                        x.If(Absence.Parameter(DC2) && Has.Value(A_KAPPA, 90)).Do(Resolve.Constant(new double?(0)));
                        x.Do(Resolve.Constant(new double?(0)));
                    });
            }
        }
    }
}