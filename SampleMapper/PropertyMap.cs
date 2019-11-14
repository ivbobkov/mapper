using System;
using System.Collections.Generic;
using System.Reflection;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public struct PropertyMap : IEquatable<PropertyMap>
    {
        public PropertyMap(PropertyInfo receiverProperty, IEnumerable<MappingAction> mappingActions)
        {
            ReceiverProperty = receiverProperty;
            MappingActions = mappingActions;
        }

        public PropertyInfo ReceiverProperty { get; }
        public IEnumerable<MappingAction> MappingActions { get; }

        public bool Equals(PropertyMap other)
        {
            return ReceiverProperty.Equals(other.ReceiverProperty);
        }

        public override bool Equals(object obj)
        {
            return obj is PropertyMap other && Equals(other);
        }

        public override int GetHashCode()
        {
            var stateHash = ReceiverProperty.GetHashCode();

            return HashCodeHelper.ResolveHashForType(stateHash, GetType());
        }
    }
}