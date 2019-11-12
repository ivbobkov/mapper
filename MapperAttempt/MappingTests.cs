using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void Test()
        {
            var mapping = new Mapping<FakeCatalog, FakeToolDto>
            {
                ExecutionClause = Has.Class(ToolClasses.M04) && Has.Group(ToolGroups.G_01),
                MappingExpressions =
                {
                    new MappingExpression<FakeCatalog, double?>
                    {
                        ReceiverMember = Expression.Parameter(typeof(double?), "PointDiameter"),
                        MappingActions =
                        {
                            new MappingAction<FakeCatalog, double?>
                            {
                                ExecutionClause = new AlwaysTrueClause(),
                                ValueResolver = Resolve.Constant(new double?(0d))
                            }
                        }
                    },
                    new MappingExpression<FakeCatalog, double?>
                    {
                        ReceiverMember = Expression.Parameter(typeof(double?), "PointAngle"),
                        MappingActions =
                        {
                            new MappingAction<FakeCatalog, double?>
                            {
                                ExecutionClause = new AlwaysTrueClause(),
                                ValueResolver = Resolve.PointFromSettingAngle("A KAPPA")
                            }
                        }
                    },
                    new MappingExpression<FakeCatalog, double?>
                    {
                        ReceiverMember = Expression.Parameter(typeof(double?), "CornerRadius"),
                        MappingActions = { }
                    }
                }
            };

            //mapping.MappingExpressions.Add(new MappingExpression<FakeCatalog, FakeToolDto>());
        }
    }
}