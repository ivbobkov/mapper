using System.Collections.Generic;

namespace SampleMapper
{
    public class ProfileMap
    {
        public ProfileMap(
            TypePair typePair,
            ICondition condition,
            bool isDefault,
            IEnumerable<PropertyMap> propertyMaps)
        {
            TypePair = typePair;
            Condition = condition;
            IsDefault = isDefault;
            PropertyMaps = propertyMaps;
        }

        public TypePair TypePair { get; }
        public ICondition Condition { get; }
        public bool IsDefault { get; }
        public IEnumerable<PropertyMap> PropertyMaps { get; }
    }
}
