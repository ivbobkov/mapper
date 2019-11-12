using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleMapper
{
    public class PropertyMap<TSource>
    {
        // MemberInfo
        public ParameterExpression ReceiverMember { get; set; }
        public List<MappingAction<TSource>> MappingActions { get; set; }
    }

    // https://github.com/AutoMapper/AutoMapper/tree/master/src/AutoMapper
    // https://www.automatetheplanet.com/specification-design-pattern/
    // https://www.automatetheplanet.com/advanced-specification-design-pattern/
    // https://github.com/vkhorikov/SpecificationPattern/blob/master/SpecificationPattern/Specifications.cs
    // https://habr.com/ru/company/edison/blog/313410/
    // https://github.com/teoadal/veloimplementations/blob/master/src/Velo/Mapping/CompiledMapper.cs
}