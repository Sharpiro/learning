namespace InterviewPrep.Core.Sorting
{
    /// <summary>
    /// Average & Worst case: O(n log n)
    /// still usually worse than quick sort though...
    /// </summary>
    public class MergeSort : ISorter
    {
        public int[] Sort(int[] list)
        {
            Sort(list, 0, list.Length - 1);
            return list;
        }

        private void Sort(int[] list, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int pivot = (lowIndex + highIndex) / 2;
                Sort(list, lowIndex, pivot);
                Sort(list, pivot + 1, highIndex);
                Merge(list, lowIndex, pivot, highIndex);
            }
        }

        private void Merge(int[] list, int lowIndex, int pivot, int highIndex)
        {
            var leftList = list.GetFrom(lowIndex, pivot);
            var rightList = list.GetFrom(pivot + 1, highIndex);
            int i = 0, j = 0, k;

            for (k = lowIndex; i < leftList.Length && j < rightList.Length; k++)
            {
                if (leftList[i] > rightList[j])
                {
                    list[k] = (rightList[j]);
                    j++;
                }
                else
                {
                    list[k] = (leftList[i]);
                    i++;
                }
            }
            while (i < leftList.Length)
            {
                list[k] = (leftList[i]);
                i++;
                k++;
            }
            while (j < rightList.Length)
            {
                list[k] = (rightList[j]);
                j++;
                k++;
            }
        }
    }

    public static class Extensions
    {
        public static int[] GetFrom(this int[] list, int startIndex, int endIndex)
        {
            var newList = new int[endIndex - startIndex + 1];
            var j = 0;
            for (var i = startIndex; i <= endIndex; i++)
            {
                newList[j] = (list[i]);
                j++;
            }
            return newList;
        }
    }
}
