using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
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
}