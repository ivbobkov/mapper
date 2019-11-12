using System.Collections.Generic;

namespace SampleMapper
{
    public class ProfileMap
    {
        public ProfileMap(TypePair typePair)
        {
            TypePair = typePair;
        }

        public TypePair TypePair { get; }
        public ICondition Condition { get; set; }
        public List<PropertyMap> PropertyMaps { get; set; }
    }
}