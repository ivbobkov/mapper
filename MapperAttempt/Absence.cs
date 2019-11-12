using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    public static class Absence
    {
        public static Condition<FakeCatalog> Parameter(string parameterName)
        {
            return new NotCondition<FakeCatalog>(new HasParameterCondition(parameterName));
        }
    }
}