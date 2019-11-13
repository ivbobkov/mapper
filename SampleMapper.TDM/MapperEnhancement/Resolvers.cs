using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper.TDM.MapperEnhancement
{
    public class DoubleValueByParameterResolver : ValueResolver<CatalogItem, double?>
    {
        public string ParameterName { get; }

        public DoubleValueByParameterResolver(string parameterName)
        {
            ParameterName = parameterName;
        }

        protected override Expression<Func<CatalogItem, double?>> CreateResolver()
        {
            return source => source.CatalogValues.Single(x => x.Code.Equals(ParameterName)).ValNum;
        }
    }

    public class StringValueByParameterResolver : ValueResolver<CatalogItem, string>
    {
        public string ParameterName { get; }

        public StringValueByParameterResolver(string parameterName)
        {
            ParameterName = parameterName;
        }

        protected override Expression<Func<CatalogItem, string>> CreateResolver()
        {
            return source => source.CatalogValues.Single(x => x.Code.Equals(ParameterName)).Val;
        }
    }

    public class ConstantValueResolver<TConstValue> : ValueResolver<CatalogItem, TConstValue>
    {
        public ConstantValueResolver(TConstValue constantValue)
        {
            ConstantValue = constantValue;
        }

        public TConstValue ConstantValue { get; }

        protected override Expression<Func<CatalogItem, TConstValue>> CreateResolver()
        {
            return _ => ConstantValue;
        }
    }

    public class SettingAngleValueResolver : ValueResolver<CatalogItem, double?>
    {
        public SettingAngleValueResolver(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }

        protected override Expression<Func<CatalogItem, double?>> CreateResolver()
        {
            throw new NotImplementedException();
        }
    }
}