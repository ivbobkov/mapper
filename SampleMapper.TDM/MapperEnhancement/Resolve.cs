namespace SampleMapper.TDM.MapperEnhancement
{
    public static class Resolve
    {
        public static ValueResolver<CatalogItem, double?> DoubleFromParameter(string parameterName)
        {
            return new DoubleValueByParameterResolver(parameterName);
        }

        public static ValueResolver<CatalogItem, TConstValue> Constant<TConstValue>(TConstValue constantValue)
        {
            return new ConstantValueResolver<TConstValue>(constantValue);
        }

        public static ValueResolver<CatalogItem, double?> PointFromSettingAngle(string parameterName)
        {
            return new SettingAngleValueResolver(parameterName);
        }

        public static ValueResolver<CatalogItem, string> StringFromParameter(string parameterName)
        {
            return new StringValueByParameterResolver(parameterName);
        }
    }
}