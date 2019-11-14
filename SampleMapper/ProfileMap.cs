using System;
using System.Collections.Generic;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public struct ProfileMap : IEquatable<ProfileMap>
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

        public bool Equals(ProfileMap other)
        {
            return
                TypePair.Equals(other.TypePair)
                && Equals(Condition, other.Condition)
                && IsDefault == other.IsDefault
                && Equals(PropertyMaps, other.PropertyMaps);
        }

        public override bool Equals(object obj)
        {
            return obj is ProfileMap other && Equals(other);
        }

        public override int GetHashCode()
        {
            var stateHash =
                TypePair.GetHashCode()
                ^ Condition.GetHashCode()
                ^ IsDefault.GetHashCode();

            return HashCodeHelper.ResolveHashForType(stateHash, GetType());
        }
    }
}
