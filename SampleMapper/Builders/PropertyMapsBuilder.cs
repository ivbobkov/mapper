using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper.Builders
{
    public class PropertyMapsBuilder<TSource, TReceiver> : IPropertyMapsBuilder<TSource, TReceiver>
    {
        private readonly List<PropertyMap> _propertyMaps = new List<PropertyMap>();

        public IPropertyMapsBuilder<TSource, TReceiver> For<TReceiverMember>(Expression<Func<TReceiver, TReceiverMember>> member, Action<IMappingActionsBuilder<TSource, TReceiverMember>> setupAction)
        {
            var propertyMapBuilder = new PropertyMapBuilder<TSource, TReceiver, TReceiverMember>();
            var propertyMap = propertyMapBuilder
                .AddMember(member)
                .AddSetupAction(setupAction)
                .Build();

            _propertyMaps.RemoveAll(x => x.ReceiverProperty.Equals(propertyMap.ReceiverProperty));
            _propertyMaps.Add(propertyMap);

            return this;
        }

        public IEnumerable<PropertyMap> Build()
        {
            if (!_propertyMaps.Any())
            {
                throw new InvalidOperationException("No property maps configured");
            }

            return _propertyMaps;
        }

        public static IPropertyMapsBuilder<TSource, TReceiver> Create()
        {
            return new PropertyMapsBuilder<TSource, TReceiver>();
        }
    }
}