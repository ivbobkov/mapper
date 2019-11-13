using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using SampleMapper.Internal;

namespace SampleMapper.Builders
{
    public class PropertyMapBuilder<TSource, TReceiver, TReceiverMember> : IPropertyMapBuilder<TSource, TReceiver, TReceiverMember>
    {
        private MemberInfo _receiverMember;
        private IEnumerable<MappingAction> _mappingActions;

        public PropertyMap Build()
        {
            if (_receiverMember == null)
            {
                throw new InvalidOperationException("You must configure receiver member before build");
            }

            if (_mappingActions == null)
            {
                throw new InvalidOperationException("You must configure mapping actions before build");
            }

            return new PropertyMap(_receiverMember, _mappingActions);
        }

        public IPropertyMapBuilder<TSource, TReceiver, TReceiverMember> AddMember(Expression<Func<TReceiver, TReceiverMember>> receiverMember)
        {
            if (receiverMember == null)
            {
                throw new ArgumentNullException(nameof(receiverMember));
            }

            _receiverMember = ReflectionHelper.FindProperty(receiverMember);

            return this;
        }

        public IPropertyMapBuilder<TSource, TReceiver, TReceiverMember> AddSetupAction(Action<IMappingActionsBuilder<TSource, TReceiverMember>> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var mappingActionsBuilder = new MappingActionsBuilder<TSource, TReceiverMember>();
            setupAction(mappingActionsBuilder);
            _mappingActions = mappingActionsBuilder.Build();

            return this;
        }
    }
}