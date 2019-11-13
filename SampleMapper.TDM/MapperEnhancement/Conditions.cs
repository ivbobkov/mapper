using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper.TDM.MapperEnhancement
{
    public class HasValueCondition<TValue> : Condition<CatalogItem>
    {
        public HasValueCondition(string parameterName, TValue parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }

        public string ParameterName { get; }
        public TValue ParameterValue { get; }

        protected override Expression<Func<CatalogItem, bool>> CreateCondition()
        {
            return catalog =>
                catalog.CatalogValues.Any(c => c.Code.Equals(ParameterName))
                && catalog.CatalogValues.Single(c => c.Code.Equals(ParameterName)).ValNum.Equals(ParameterValue);
        }
    }

    public class HasParameterCondition : Condition<CatalogItem>
    {
        public HasParameterCondition(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }

        protected override Expression<Func<CatalogItem, bool>> CreateCondition()
        {
            return catalog => catalog.HasParameter(ParameterName);
        }
    }

    public class HasParametersCondition : Condition<CatalogItem>
    {
        public IEnumerable<string> ParameterNames { get; }

        public HasParametersCondition(IEnumerable<string> parameterNames)
        {
            ParameterNames = parameterNames;
        }

        protected override Expression<Func<CatalogItem, bool>> CreateCondition()
        {
            return catalog => ParameterNames.All(x => catalog.CatalogValues.Any(c => c.Code.Equals(x)));
        }
    }

    public class HasClassCondition : Condition<CatalogItem>
    {
        public string ClassName { get; }

        public HasClassCondition(string className)
        {
            ClassName = className;
        }

        protected override Expression<Func<CatalogItem, bool>> CreateCondition()
        {
            return catalog => catalog.ToolClassId.Equals(ClassName);
        }
    }

    public class HasGroupCondition : Condition<CatalogItem>
    {
        public string GroupName { get; }

        public HasGroupCondition(string groupName)
        {
            GroupName = groupName;
        }

        protected override Expression<Func<CatalogItem, bool>> CreateCondition()
        {
            return catalog => catalog.ToolGroupId.Equals(GroupName);
        }
    }
}
