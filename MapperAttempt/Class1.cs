using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using TinyMapper.Core2;
using TinyMapper.Tests.LiveSamples;
using static TinyMapper.Tests.LiveSamples.ToolClasses;
using static TinyMapper.Tests.LiveSamples.ToolGroups;
using static TinyMapper.Tests.LiveSamples.ToolClassFields;

namespace TinyMapper.Tests
{
    public class HasValueClause<TValue> : Clause<FakeCatalog>
    {
        public HasValueClause(string parameterName, TValue parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }

        public string ParameterName { get; }
        public TValue ParameterValue { get; }

        public override Expression<Func<FakeCatalog, bool>> ToExpression()
        {
            Expression<Func<FakeCatalog, bool>> expression = catalog =>
                catalog.CatalogValues.Any(c => c.Code.Equals(ParameterName))
                && catalog.CatalogValues.Single(c => c.Code.Equals(ParameterName)).ValNum.Equals(ParameterValue);

            return expression;
        }
    }

    public class HasParameterClause : Clause<FakeCatalog>
    {
        public HasParameterClause(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }

        public override Expression<Func<FakeCatalog, bool>> ToExpression()
        {
            return catalog => catalog.Has(ParameterName);
        }
    }

    public class HasParametersClause : Clause<FakeCatalog>
    {
        public IEnumerable<string> ParameterNames { get; }

        public HasParametersClause(IEnumerable<string> parameterNames)
        {
            ParameterNames = parameterNames;
        }

        public override Expression<Func<FakeCatalog, bool>> ToExpression()
        {
            return catalog => ParameterNames.All(x => catalog.CatalogValues.Any(c => c.Code.Equals(x)));
        }
    }

    public class HasClassClause : Clause<FakeCatalog>
    {
        public string ClassName { get; }

        public HasClassClause(string className)
        {
            ClassName = className;
        }

        public override Expression<Func<FakeCatalog, bool>> ToExpression()
        {
            return catalog => catalog.ToolClassId.Equals(ClassName);
        }
    }

    public class HasGroupClause : Clause<FakeCatalog>
    {
        public string GroupName { get; }

        public HasGroupClause(string groupName)
        {
            GroupName = groupName;
        }

        public override Expression<Func<FakeCatalog, bool>> ToExpression()
        {
            return catalog => catalog.ToolGroupId.Equals(GroupName);
        }
    }

    public static class Absence
    {
        public static Clause<FakeCatalog> Parameter(string parameterName)
        {
            return new NotClause<FakeCatalog>(new HasParameterClause(parameterName));
        }
    }

    public static class Has
    {
        public static Clause<FakeCatalog> Parameter(string parameterName)
        {
            return new HasParameterClause(parameterName);
        }

        public static Clause<FakeCatalog> Parameters(params string[] parameterNames)
        {
            return new HasParametersClause(parameterNames);
        }

        public static Clause<FakeCatalog> Value<TValue>(string parameterName, TValue parameterValue)
        {
            return new HasValueClause<TValue>(parameterName, parameterValue);
        }

        public static Clause<FakeCatalog> Class(string className)
        {
            return new HasClassClause(className);
        }

        public static Clause<FakeCatalog> Group(string groupName)
        {
            return new HasGroupClause(groupName);
        }
    }

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