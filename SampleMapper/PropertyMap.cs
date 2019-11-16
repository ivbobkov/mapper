using System.Collections.Generic;
using System.Reflection;

namespace SampleMapper
{
    public class PropertyMap
    {
        public PropertyMap(
            TypePair typePair,
            PropertyInfo receiverProperty,
            IEnumerable<MappingAction> mappingActions)
        {
            TypePair = typePair;
            ReceiverProperty = receiverProperty;
            MappingActions = mappingActions;
        }

        public TypePair TypePair { get; }
        public PropertyInfo ReceiverProperty { get; }
        public IEnumerable<MappingAction> MappingActions { get; }
    }
}