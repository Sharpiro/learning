using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Sorting
{
    public class MergeSort : ISorter
    {
        public IEnumerable<int> Sort(IEnumerable<int> list)
        {
            return SortX(list.ToList());
        }

        private List<int> SortX(List<int> list)
        {
            if (list.Count < 2)
                return list;
            var mid = (list.Count - 1) / 2;
            var leftList = GetFrom(list, 0, mid);
            var rightList = GetFrom(list, mid + 1, list.Count - 1);
            leftList = SortX(leftList);
            rightList = SortX(rightList);
            return Merge(leftList, rightList);
        }

        private List<int> Merge(List<int> leftList, List<int> rightList)
        {
            var mergedList = new List<int>();
            var i = 0;
            var j = 0;
            var k = 0;
            while (i < leftList.Count && j < rightList.Count)
            {
                if (leftList[i] > rightList[j])
                {
                    mergedList.Add(rightList[j]);
                    j++;
                    k++;
                }
                else
                {
                    mergedList.Add(leftList[i]);
                    i++;
                    k++;
                }
            }
            while (i < leftList.Count)
            {
                mergedList.Add(leftList[i]);
                i++;
                k++;
            }
            while (j < rightList.Count)
            {
                mergedList.Add(rightList[j]);
                j++;
                k++;
            }
            return mergedList;
        }

        private List<int> GetFrom(List<int> list, int startIndex, int endIndex)
        {
            var newList = new List<int>();
            for (var i = startIndex; i <= endIndex; i++)
            {
                newList.Add(list[i]);
            }
            return newList;
        }
    }
}
