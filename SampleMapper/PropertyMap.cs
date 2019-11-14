using System;
using System.Collections.Generic;
using System.Reflection;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public class PropertyMap : IEquatable<PropertyMap>
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
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ReceiverProperty.Equals(other.ReceiverProperty);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is PropertyMap other && Equals(other);
        }

        public override int GetHashCode()
        {
            var stateHash = ReceiverProperty.GetHashCode();

            return HashCodeHelper.ResolveHashForType(stateHash, GetType());
        }
    }
}