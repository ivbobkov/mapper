using System.Collections.Generic;
using System.Reflection;

namespace SampleMapper
{
    public class PropertyMap
    {
        public PropertyMap(MemberInfo receiverMember, IEnumerable<MappingAction> mappingActions)
        {
            ReceiverMember = receiverMember;
            MappingActions = mappingActions;
        }

        public MemberInfo ReceiverMember { get; }
        public IEnumerable<MappingAction> MappingActions { get; }
    }
}