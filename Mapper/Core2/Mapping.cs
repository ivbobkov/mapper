using System.Collections.Generic;

namespace TinyMapper.Core2
{
    public class Mapping<TSource, TReceiver>
    {
        public Clause<TSource> MappingClause { get; set; }
        public List<MappingExpression<TSource, TReceiver>> MappingExpressions { get; } = new List<MappingExpression<TSource, TReceiver>>();
    }
}