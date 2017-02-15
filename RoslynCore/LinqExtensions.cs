using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<int> SingleList => new int[1];

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T findItem) where T : IEquatable<T>
        {
            return enumerable.Select((item, index) => new { Index = index, IsEqual = item.Equals(findItem) })
                .FirstOrDefault(i => i.IsEqual)?.Index ?? -1;
        }

        public static ImmutableList<T> NotNullList<T>(params T[] parameters)
        {
            return parameters.Where(p => p != null).ToImmutableList();
        }
    }
}