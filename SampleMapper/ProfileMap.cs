using System.Collections.Generic;
using System.Reflection;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public class ProfileMap
    {
        public ProfileMap(
            TypePair typePair,
            ConstructorInfo receiverConstructorInfo,
            ICondition executionClause,
            bool isDefault,
            IEnumerable<PropertyMap> propertyMaps)
        {
            TypePair = typePair;
            ReceiverConstructorInfo = receiverConstructorInfo;
            ExecutionClause = executionClause;
            IsDefault = isDefault;
            PropertyMaps = propertyMaps;

            Identity = ComputeIdentity();
        }

        public int Identity { get; }
        public TypePair TypePair { get; }
        public ConstructorInfo ReceiverConstructorInfo { get; }
        public ICondition ExecutionClause { get; }
        public bool IsDefault { get; }
        public IEnumerable<PropertyMap> PropertyMaps { get; }

        private int ComputeIdentity()
        {
            var stateHash = TypePair.GetHashCode()
                            ^ ReceiverConstructorInfo.GetHashCode()
                            ^ ExecutionClause.GetHashCode()
                            ^ IsDefault.GetHashCode();

            return HashCodeHelper.ResolveHashForType(stateHash, GetType());
        }
    }
}
