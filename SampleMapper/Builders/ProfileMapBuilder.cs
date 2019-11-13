using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
                throw new InvalidOperationException("Impossible to set execution clause to default profile.");
            }

            _executionClause = executionClause;

            return this;
        }

        public IProfileMapBuilder<TSource, TReceiver> UseAsDefaultProfile()
        {
            if (_executionClause != null)
            {
                throw new InvalidOperationException("Impossible to set is default for profile that already has execution clause.");
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

            // TODO: 1) check for duplicate
            // TODO: 2) replace duplicate new PropertyMap

            _propertyMaps.Add(propertyMap);

            return this;
        }

        public ProfileMap Build()
        {
            if (_executionClause == null)
            {
                throw new InvalidOperationException("Execution clause must be set");
            }

            var typePair = TypePair.Create<TSource, TReceiver>();

            return new ProfileMap(typePair, _executionClause, _isDefault, _propertyMaps);
        }
    }
}