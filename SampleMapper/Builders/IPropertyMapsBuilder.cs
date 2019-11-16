using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SampleMapper.Builders
{
    public interface IPropertyMapsBuilder<TSource, TReceiver>
    {
        IPropertyMapsBuilder<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> member,
            Action<IMappingActionsBuilder<TSource, TReceiverMember>> setupAction);

        IEnumerable<PropertyMap> Build();
    }
}