using System.Collections.Generic;
using System.Reflection;

namespace SampleMapper
{
    public class PropertyMap
    {
        public PropertyMap(PropertyInfo receiverProperty, IEnumerable<MappingAction> mappingActions)
        {
            ReceiverProperty = receiverProperty;
            MappingActions = mappingActions;
        }

        public PropertyInfo ReceiverProperty { get; }
        public IEnumerable<MappingAction> MappingActions { get; }
    }
}