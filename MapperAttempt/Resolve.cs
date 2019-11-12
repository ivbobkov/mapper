using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    public static class Resolve
    {
        public static ValueResolver<FakeCatalog, double?> DoubleFromParameter(string parameterName)
        {
            return new DoubleValueByParameterResolver(parameterName);
        }

        public static ValueResolver<FakeCatalog, TConstValue> Constant<TConstValue>(TConstValue constantValue)
        {
            return new ConstantValueResolver<TConstValue>(constantValue);
        }

        public static ValueResolver<FakeCatalog, double?> PointFromSettingAngle(string parameterName)
        {
            return new SettingAngleValueResolver(parameterName);
        }

        public static ValueResolver<FakeCatalog, string> StringFromParameter(string parameterName)
        {
            return new StringValueByParameterResolver(parameterName);
        }
    }
}