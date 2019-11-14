using System;
using SampleMapper.Helpers;

namespace SampleMapper
{
    public struct TypePair : IEquatable<TypePair>
    {
        public TypePair(Type sourceType, Type receiverType)
        {
            SourceType = sourceType;
            ReceiverType = receiverType;
        }

        public Type SourceType { get; }

        public Type ReceiverType { get; }

        public static TypePair Create<TSource, TReceiver>()
        {
            return new TypePair(typeof(TSource), typeof(TReceiver));
        }

        public bool Equals(TypePair other)
        {
            return SourceType == other.SourceType && ReceiverType == other.ReceiverType;
        }

        public override bool Equals(object obj)
        {
            return obj is TypePair other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.ResolveHashForType(SourceType.GetHashCode(), ReceiverType);
        }
    }
}