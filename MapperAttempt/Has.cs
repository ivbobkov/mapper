using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    public static class Has
    {
        public static Condition<FakeCatalog> Parameter(string parameterName)
        {
            return new HasParameterCondition(parameterName);
        }

        public static Condition<FakeCatalog> Parameters(params string[] parameterNames)
        {
            return new HasParametersCondition(parameterNames);
        }

        public static Condition<FakeCatalog> Value<TValue>(string parameterName, TValue parameterValue)
        {
            return new HasValueCondition<TValue>(parameterName, parameterValue);
        }

        public static Condition<FakeCatalog> Class(string className)
        {
            return new HasClassCondition(className);
        }

        public static Condition<FakeCatalog> Group(string groupName)
        {
            return new HasGroupCondition(groupName);
        }
    }
}