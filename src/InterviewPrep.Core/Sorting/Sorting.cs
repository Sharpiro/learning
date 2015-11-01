using System.Collections.Generic;

namespace InterviewPrep.Core.Sorting
{
    /// <summary>
    /// Can be unstable or stable: order is not preserved or preserved respectively if items are equal
    /// Average case: O(n log n)
    /// Worst case: O(n^2)
    /// used by LINQ 2 Objects "OrderBy"
    /// </summary>
    public static class Sorting
    {
        public static IEnumerable<int> QuickSortEasy(int[] list, int low, int high)
        {
            if (low < high)
            {
                var pivot = Partition(list, low, high);
                QuickSortEasy(list, low, pivot - 1);
                QuickSortEasy(list, pivot + 1, high);
            }
            return list;
        }

        private static int Partition(int[] list, int low, int high)
        {
            var pivot = list[high];
            var globalIndex = low;
            for (var i = low; i < high; i++)
            {
                if (list[i] <= pivot)
                {
                    var temp = list[i];
                    list[i] = list[globalIndex];
                    list[globalIndex] = temp;
                    globalIndex++;
                }
            }
            var temp2 = list[high];
            list[high] = list[globalIndex];
            list[globalIndex] = temp2;
            return globalIndex;
        }
    }
}
