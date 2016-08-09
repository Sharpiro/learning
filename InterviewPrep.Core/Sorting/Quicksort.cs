using System;

namespace InterviewPrep.Core.Sorting
{
    public class QuickSort : ISorter
    {
        public int[] Sort(int[] list)
        {
            var newList = new int[list.Length];
            Array.Copy(list, newList, list.Length);
            Sort(newList, 0, list.Length - 1);
            return newList;
        }

        public void Sort(int[] list, int lowIndex, int highIndex)
        {
            if (lowIndex >= highIndex)
                return;
            var pivot = Partition(list, lowIndex, highIndex);
            Sort(list, lowIndex, pivot - 1);
            Sort(list, pivot + 1, highIndex);
        }

        private int Partition(int[] list, int lowIndex, int highIndex)
        {
            var newPivotIndex = lowIndex;

            for (var i = lowIndex; i < highIndex; i++)
            {
                if (list[i] < list[highIndex])
                {
                    Swap(ref list[i], ref list[newPivotIndex]);
                    newPivotIndex++;
                }
            }
            Swap(ref list[newPivotIndex], ref list[highIndex]);
            return newPivotIndex;
        }

        private void Swap(ref int x, ref int y)
        {
            var temp = x;
            x = y;
            y = temp;
        }
    }
}
