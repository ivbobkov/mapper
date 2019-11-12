using System;
using System.Linq;
using System.Linq.Expressions;
using SampleMapper;
using TinyMapper.Tests.LiveSamples;

namespace TinyMapper.Tests
{
    public class DoubleValueByParameterResolver : ValueResolver<FakeCatalog, double?>
    {
        public string ParameterName { get; }

        public DoubleValueByParameterResolver(string parameterName)
        {
            ParameterName = parameterName;
        }

        public override Expression<Func<FakeCatalog, double?>> ToExpression()
        {
            return source => source.CatalogValues.Single(x => x.Code.Equals(ParameterName)).ValNum;
        }
    }

    public class StringValueByParameterResolver : ValueResolver<FakeCatalog, string>
    {
        public string ParameterName { get; }

        public StringValueByParameterResolver(string parameterName)
        {
            ParameterName = parameterName;
        }

        public override Expression<Func<FakeCatalog, string>> ToExpression()
        {
            return source => source.CatalogValues.Single(x => x.Code.Equals(ParameterName)).Val;
        }
    }

    public class ConstantValueResolver<TConstValue> : ValueResolver<FakeCatalog, TConstValue>
    {
        public ConstantValueResolver(TConstValue constantValue)
        {
            ConstantValue = constantValue;
        }

        public TConstValue ConstantValue { get; }

        public override Expression<Func<FakeCatalog, TConstValue>> ToExpression()
        {
            return _ => ConstantValue;
        }
    }

    public class SettingAngleValueResolver : ValueResolver<FakeCatalog, double?>
    {
        public SettingAngleValueResolver(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }

        public override Expression<Func<FakeCatalog, double?>> ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}