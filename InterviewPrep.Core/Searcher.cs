using System;
using System.Collections.Generic;

namespace InterviewPrep.Core
{
    public static class Searcher
    {
        /// <summary>
        /// O(n) time complexity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static (T Item, int Index) LinearSearch<T>(IList<T> list, T value) where T : IComparable<T>
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(value))
                {
                    return (list[i], i);
                }
            }
            return (default(T), -1);
        }

        /// <summary>
        /// O(log(n)) time complexity
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int BinarySearch(IList<int> list, int value)
        {
            var low = 0;
            var high = list.Count - 1;
            var pivot = (low + high) / 2;

            while (low <= high)
            {
                if (value == list[pivot]) return list[pivot];
                if (value < list[pivot])
                    high = pivot - 1;
                else if (value > list[pivot])
                    low = pivot + 1;
                pivot = (low + high) / 2;
            }
            return -1;
        }

        /// <summary>
        /// O(log(n)) time complexity
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int BinarySearchRecursive(IList<int> list, int value, int low, int high)
        {
            if (low > high) return -1;
            var pivot = (low + high) / 2;
            if (value == list[pivot]) return list[pivot];
            if (value < list[pivot])
                return BinarySearchRecursive(list, value, low, pivot - 1);
            return BinarySearchRecursive(list, value, pivot + 1, high);
        }
    }
}
//Linear Search: Search through the entire array from front to back or until match is found.  O(n) time complexity