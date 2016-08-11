using System;
using System.Collections.Generic;

namespace InterviewPrep.LinqFundamentals
{
    public static class LinqExtensions
    {
        public static int CountX<T>(this IEnumerable<T> list)
        {
            var counter = 0;
            foreach (var item in list)
            {
                counter++;
            }
            return counter;
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> func)
        {
            var newList = new List<T>();
            //var temp = new int[list.
            foreach (var item in list)
            {
                if (func(item))
                {
                    newList.Add(item);
                }
            }
            return newList;
        }
    }
}
