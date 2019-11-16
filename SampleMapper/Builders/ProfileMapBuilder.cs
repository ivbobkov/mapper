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

        public IProfileMapBuilder<TSource, TReceiver> UseAsDefault()
        {
            if (_executionClause != null)
            {
                throw new InvalidOperationException("Profile with execution clause could not be default");
            }

            _isDefault = true;
            _executionClause = new BlankCondition<TSource>();

            return this;
        }

        public IProfileMapBuilder<TSource, TReceiver> UsePropertyMaps(IEnumerable<PropertyMap> propertyMaps)
        {
            if (propertyMaps == null)
            {
                throw new ArgumentNullException(nameof(propertyMaps));
            }

            var typePair = TypePair.Create<TSource, TReceiver>();
            var propertyMapsList = propertyMaps.ToList();

            if (!propertyMapsList.Any(c => c.TypePair.Equals(typePair)))
            {
                throw new ArgumentException("Property map for different type pair received");
            }

            foreach (var propertyMap in propertyMapsList)
            {
                AddOrReplacePropertyMap(propertyMap);
            }

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

            AddOrReplacePropertyMap(propertyMap);

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

        private void AddOrReplacePropertyMap(PropertyMap propertyMap)
        {
            _propertyMaps.RemoveAll(x => x.ReceiverProperty.Equals(propertyMap.ReceiverProperty));
            _propertyMaps.Add(propertyMap);
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