using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    public class HasValueCondition<TValue> : Condition<FakeCatalog>
    {
        public HasValueCondition(string parameterName, TValue parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }

        public string ParameterName { get; }
        public TValue ParameterValue { get; }

        public override Expression<Func<FakeCatalog, bool>> CreateCondition()
        {
            return catalog =>
                catalog.CatalogValues.Any(c => c.Code.Equals(ParameterName))
                && catalog.CatalogValues.Single(c => c.Code.Equals(ParameterName)).ValNum.Equals(ParameterValue);
        }
    }

    public class HasParameterCondition : Condition<FakeCatalog>
    {
        public HasParameterCondition(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }

        public override Expression<Func<FakeCatalog, bool>> CreateCondition()
        {
            return catalog => catalog.Has(ParameterName);
        }
    }

    public class HasParametersCondition : Condition<FakeCatalog>
    {
        public IEnumerable<string> ParameterNames { get; }

        public HasParametersCondition(IEnumerable<string> parameterNames)
        {
            ParameterNames = parameterNames;
        }

        public override Expression<Func<FakeCatalog, bool>> CreateCondition()
        {
            return catalog => ParameterNames.All(x => catalog.CatalogValues.Any(c => c.Code.Equals(x)));
        }
    }

    public class HasClassCondition : Condition<FakeCatalog>
    {
        public string ClassName { get; }

        public HasClassCondition(string className)
        {
            ClassName = className;
        }

        public override Expression<Func<FakeCatalog, bool>> CreateCondition()
        {
            return catalog => catalog.ToolClassId.Equals(ClassName);
        }
    }

    public class HasGroupCondition : Condition<FakeCatalog>
    {
        public string GroupName { get; }

        public HasGroupCondition(string groupName)
        {
            GroupName = groupName;
        }

        public override Expression<Func<FakeCatalog, bool>> CreateCondition()
        {
            return catalog => catalog.ToolGroupId.Equals(GroupName);
        }
    }

    public class BlankCondition : Condition<FakeCatalog>
    {
        public override Expression<Func<FakeCatalog, bool>> CreateCondition()
        {
            return _ => true;
        }
    }
}
