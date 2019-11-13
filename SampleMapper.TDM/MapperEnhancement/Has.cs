namespace SampleMapper.TDM.MapperEnhancement
{
    public static class Has
    {
        public static Condition<CatalogItem> Parameter(string parameterName)
        {
            return new HasParameterCondition(parameterName);
        }

        public static Condition<CatalogItem> Parameters(params string[] parameterNames)
        {
            return new HasParametersCondition(parameterNames);
        }

        public static Condition<CatalogItem> Value<TValue>(string parameterName, TValue parameterValue)
        {
            return new HasValueCondition<TValue>(parameterName, parameterValue);
        }

        public static Condition<CatalogItem> Class(string className)
        {
            return new HasClassCondition(className);
        }

        public static Condition<CatalogItem> Group(string groupName)
        {
            return new HasGroupCondition(groupName);
        }
    }
}