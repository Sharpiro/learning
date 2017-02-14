using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<int> SingleList => new int[] { 0 };

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T findItem) where T : IEquatable<T>
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                if (findItem.Equals(item))
                    return index;
                index++;
            }
            return -1;
        }

        public static ImmutableList<T> NotNullList<T>(params T[] parameters)
        {
            return parameters.Where(p => p != null).ToImmutableList();
        }
    }
}