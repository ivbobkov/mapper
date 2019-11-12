using System;

namespace SampleMapper
{
    public struct TypePair 
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

        // TODO: use IEquatable<TypePair> here
    }
}