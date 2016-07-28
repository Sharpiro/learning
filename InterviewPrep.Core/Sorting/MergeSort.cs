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
            if (list.Length == 1)
                return list;
            var mid = (list.Length - 1) / 2;
            var leftList = GetFrom(list, 0, mid);
            var rightList = GetFrom(list, mid + 1, list.Length - 1);
            leftList = Sort(leftList);
            rightList = Sort(rightList);
            return Merge(leftList, rightList);
        }

        private int[] Merge(int[] leftList, int[] rightList)
        {
            var mergedList = new int[leftList.Length + rightList.Length];
            var i = 0;
            var j = 0;
            var k = 0;
            while (i < leftList.Length && j < rightList.Length)
            {
                if (leftList[i] > rightList[j])
                {
                    mergedList[k] = (rightList[j]);
                    j++;
                    k++;
                }
                else
                {
                    mergedList[k] = (leftList[i]);
                    i++;
                    k++;
                }
            }
            while (i < leftList.Length)
            {
                mergedList[k] = (leftList[i]);
                i++;
                k++;
            }
            while (j < rightList.Length)
            {
                mergedList[k] = (rightList[j]);
                j++;
                k++;
            }
            return mergedList;
        }

        private int[] GetFrom(int[] list, int startIndex, int endIndex)
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
