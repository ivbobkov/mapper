using System.Collections.Generic;
using System.Linq.Expressions;

namespace TinyMapper.Core2
{
    public class MappingExpression<TSource, TReceiver>
    {
        public ParameterExpression ReceiverMember { get; }
        public IEnumerable<MappingAction> MappingActions { get; }
    }
}