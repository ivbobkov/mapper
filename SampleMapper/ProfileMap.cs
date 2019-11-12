using System.Collections.Generic;

namespace SampleMapper
{
    public class ProfileMap
    {
        public TypePair TypePair { get; set; }
        public ICondition Condition { get; set; }
        public List<PropertyMap> PropertyMaps { get; set; }
    }
}