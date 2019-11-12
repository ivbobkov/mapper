using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    public static class Absence
    {
        public static Clause<FakeCatalog> Parameter(string parameterName)
        {
            return new NotClause<FakeCatalog>(new HasParameterClause(parameterName));
        }
    }
}