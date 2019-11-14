using System;
using System.Collections.Concurrent;

namespace SampleMapper.Helpers
{
    public static class HashCodeHelper
    {
        private static readonly ConcurrentDictionary<Type, int> TypeDeviations = new ConcurrentDictionary<Type, int>();

        public static int ResolveHashForType(int hash, Type type)
        {
            unchecked
            {
                var deviation = GetDeviationForType(type);
                var typeHash = type.GetHashCode();
            
                return ((hash << deviation) + hash) ^ typeHash;
            }
        }

        private static int GetDeviationForType(Type type)
        {
            if (TypeDeviations.TryGetValue(type, out var entry))
            {
                return entry;
            }

            var typeDeviation = Guid.NewGuid().GetHashCode();

            if (TypeDeviations.TryAdd(type, typeDeviation))
            {
                return typeDeviation;
            }
            else
            {
                return TypeDeviations[type];
            }
        }
    }
}