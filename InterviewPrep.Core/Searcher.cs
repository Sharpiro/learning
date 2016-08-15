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
        /// <param name="index"></param>
        /// <returns></returns>
        public static T LinearSearch<T>(IList<T> list, T value, out int index)
        {
            index = -1;
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(value))
                {
                    index = i;
                    break;
                }
            }
            var output = index == -1 ? list[0] : list[index];
            return output;
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
            var midPoint = (int)Math.Floor(((double)low + high) / 2);
            while (low <= high)
            {
                if (list[midPoint] == value)
                    return midPoint;
                if (list[midPoint] < value)
                    low = midPoint + 1;
                else if (list[midPoint] > value)
                    high = midPoint - 1;
                midPoint = (int)Math.Floor(((double)low + high) / 2);
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
            if (value == list[pivot])
                return pivot;
            if (value < list[pivot])
                return BinarySearchRecursive(list, value, low, pivot - 1);
            else
                return BinarySearchRecursive(list, value, pivot + 1, high);
        }
    }
}
//Linear Search: Search through the entire array from front to back or until match is found.  O(n) time complexity