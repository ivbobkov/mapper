using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;

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
            return catalog =>
                catalog.CatalogValues.Any(c => c.Code.Equals(ParameterName))
                && catalog.CatalogValues.Single(c => c.Code.Equals(ParameterName)).ValNum.Equals(ParameterValue);
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

    public class AlwaysTrueClause : Clause<FakeCatalog>
    {
        public override Expression<Func<FakeCatalog, bool>> ToExpression()
        {
            return _ => true;
        }
    }
}
