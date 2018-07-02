using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Sorting
{
    public class MergeSort : ISorter
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
            var pivot = (lowIndex + highIndex) / 2;
            Sort(list, lowIndex, pivot);
            Sort(list, pivot + 1, highIndex);
            Merge(list, lowIndex, pivot, highIndex);
        }

        private void Merge(int[] list, int lowIndex, int mid, int highIndex)
        {
            var leftList = list.SubList(lowIndex, mid).ToArray();
            var rightList = list.SubList(mid + 1, highIndex).ToArray();

            int i = 0, j = 0, k;
            for (k = lowIndex; i < leftList.Length && j < rightList.Length; k++)
            {
                if (leftList[i] <= rightList[j])
                {
                    list[k] = leftList[i];
                    i++;
                }
                else
                {
                    list[k] = rightList[j];
                    j++;
                }
            }

            while (i < leftList.Length)
            {
                list[k] = leftList[i];
                i++;
                k++;
            }

            while (j < rightList.Length)
            {
                list[k] = rightList[j];
                j++;
                k++;
            }
        }
    }

    public static class MergeExtensions
    {
        public static IEnumerable<int> SubList(this int[] list, int lowIndex, int highIndex)
        {
            for (int i = lowIndex, j = 0; i <= highIndex; i++, j++)
            {
                yield return list[i];
            }
        }
    }
}
