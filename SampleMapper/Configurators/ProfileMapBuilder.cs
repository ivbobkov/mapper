using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleMapper.Configurators
{
    public class ProfileMapBuilder<TSource, TReceiver> : IProfileMapBuilder<TSource, TReceiver>
    {
        private Condition<TSource> _condition;
        private List<PropertyMap> _inheritedPropertyMaps = new List<PropertyMap>();
        private List<PropertyMap> _propertyMaps = new List<PropertyMap>();

        public IProfileMapBuilder<TSource, TReceiver> Condition(Condition<TSource> condition)
        {
            _condition = condition;

            return this;
        }

        public IProfileMapBuilder<TSource, TReceiver> Include(params PropertyMap[] propertyMaps)
        {
            // TODO: check for initialization? check for duplicates?
            if (propertyMaps == null)
            {
                throw new ArgumentNullException(nameof(propertyMaps));
            }

            _inheritedPropertyMaps.AddRange(propertyMaps);

            return this;
        }

        public IProfileMapBuilder<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> receiverMember,
            Action<IMemberMapsBuilder<TSource, TReceiverMember>> configuratorAction)
        {
            var configurator = new MemberMapsBuilder<TSource, TReceiverMember>();
            configuratorAction(configurator);

            var propertyMap = new PropertyMap
            {
                ReceiverMember = receiverMember.Parameters.Single(),
                MemberMaps = configurator.MemberMaps
            };

            _propertyMaps.Add(propertyMap);

            return this;
        }
    }
}