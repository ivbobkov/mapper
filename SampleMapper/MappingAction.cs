using System;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public struct MappingAction : IEquatable<MappingAction>
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
            return ExecutionClause.Equals(other.ExecutionClause);
        }

        public override bool Equals(object obj)
        {
            return obj is MappingAction other && Equals(other);
        }

        public override int GetHashCode()
        {
            var stateHash = ExecutionClause.GetHashCode();

            return HashCodeHelper.ResolveHashForType(stateHash, GetType());
        }
    }
}