using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Linq
{
    public static class GraphExtensions
    {
        public static T RemoveMin<T>(this List<T> list) where T : IComparable<T>
        {
            var minItem = list.Min();
            list.Remove(minItem);
            return minItem;
        }
    }
}
