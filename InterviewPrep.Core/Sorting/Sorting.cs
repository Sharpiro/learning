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
        public static IEnumerable<int> QuickSortEasy(int[] list, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var pivotIndex = PartitionEasy(list, lowIndex, highIndex);
                QuickSortEasy(list, lowIndex, pivotIndex - 1);
                QuickSortEasy(list, pivotIndex + 1, highIndex);
            }
            return list;
        }

        private static int PartitionEasy(int[] list, int lowIndex, int highIndex)
        {
            var pivotValue = list[highIndex];
            var globalIndex = lowIndex;
            for (var i = lowIndex; i < highIndex; i++)
            {
                var currentValue = list[i];
                if (currentValue < pivotValue)
                {
                    Swap(ref list[i], ref list[globalIndex]);
                    globalIndex++;
                }
            }
            Swap(ref list[highIndex], ref list[globalIndex]);
            return globalIndex;
        }

        private static void Swap(ref int number1, ref int number2)
        {
            var temp = number1;
            number1 = number2;
            number2 = temp;
        }
    }
}
