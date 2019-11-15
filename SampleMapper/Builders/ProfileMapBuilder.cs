using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SampleMapper.Builders
{
    public class ProfileMapBuilder<TSource, TReceiver> : IProfileMapBuilder<TSource, TReceiver>
    {
        private Condition<TSource> _executionClause;
        private bool _isDefault;
        private readonly List<PropertyMap> _propertyMaps = new List<PropertyMap>();

        public IProfileMapBuilder<TSource, TReceiver> UseExecutionClause(Condition<TSource> executionClause)
        {
            if (executionClause == null)
            {
                throw new ArgumentNullException(nameof(executionClause));
            }

            if (_isDefault)
            {
                throw new InvalidOperationException("You could not use execution clause for default profile");
            }

            _executionClause = executionClause;

            return this;
        }

        public IProfileMapBuilder<TSource, TReceiver> UseAsDefaultProfile()
        {
            if (_executionClause != null)
            {
                throw new InvalidOperationException("Profile with execution clause could not be default");
            }

            _isDefault = true;
            _executionClause = new BlankCondition<TSource>();

            return this;
        }

        public IProfileMapBuilder<TSource, TReceiver> For<TReceiverMember>(
            Expression<Func<TReceiver, TReceiverMember>> member,
            Action<IMappingActionsBuilder<TSource, TReceiverMember>> setupAction)
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

        public ProfileMap Build()
        {
            if (_executionClause == null)
            {
                throw new InvalidOperationException("Execution clause must be set for non default profile");
            }

            if (!_propertyMaps.Any())
            {
                throw new InvalidOperationException("No property maps configured");
            }

            var typePair = TypePair.Create<TSource, TReceiver>();

            return new ProfileMap(
                typePair,
                GetReceiverConstructorInfo(typePair.ReceiverType),
                _executionClause,
                _isDefault,
                _propertyMaps);
        }

        private ConstructorInfo GetReceiverConstructorInfo(Type receiverType)
        {
            var receiverConstructor = receiverType.GetConstructor(Array.Empty<Type>());

            if (receiverConstructor == null)
            {
                throw new ArgumentException($"Default constructor for {receiverType.Name} not found");
            }

            return receiverConstructor;
        }
    }
}