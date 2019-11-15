namespace SampleMapper.TDM.MapperEnhancement
{
    public static class Absence
    {
        public static Condition<CatalogItem> Parameter(string parameterName)
        {
            return !new HasParameterCondition(parameterName);
        }
    }
}