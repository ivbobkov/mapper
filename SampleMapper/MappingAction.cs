using System;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public class MappingAction : IEquatable<MappingAction>
    {
        public MappingAction(ICondition executionClause, IValueResolver valueResolver)
        {
            ExecutionClause = executionClause;
            ValueResolver = valueResolver;
        }

        public ICondition ExecutionClause { get; }
        public IValueResolver ValueResolver { get; }

        public bool Equals(MappingAction other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return ExecutionClause.Equals(other.ExecutionClause);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is MappingAction other && Equals(other);
        }

        public override int GetHashCode()
        {
            var stateHash = ExecutionClause.GetHashCode();

            return HashCodeHelper.ResolveHashForType(stateHash, GetType());
        }
    }
}