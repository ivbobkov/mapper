using System.Collections.Generic;

namespace SampleMapper
{
    public class ProfileMap<TSource, TReceiver>
    {
        //public MemberInfo MemberInfo { get; set; }

        public Clause<TSource> Condition { get; set; }
        public List<PropertyMap<TSource>> MappingExpressions { get; set; }
    }
}