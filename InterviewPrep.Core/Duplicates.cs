using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core
{
    public static class Duplicates
    {
        public static bool HasDupesIndex(List<int> list)
        {
            var index = new BitArray(int.MaxValue);
            foreach (var i in list)
            {
                if (index[i])
                    return true;
                index[i] = true;
            }
            return false;
        }

        public static bool HasDupesHashSet(List<int> list)
        {
            var hashSet = new HashSet<int>();
            for (var i = 0; i < list.Count; i++)
            {
                if (hashSet.Contains(list[i]))
                    return true;
                hashSet.Add(list[i]);
            }
            return false;
        }

        public static bool HasDupesSortFirst(List<int> list)
        {
            list.Sort();
            for (var i = 0; i < list.Count - 1; i++)
            {
                if (list[i] == list[i + 1])
                    return true;
            }
            return false;
        }

        public static bool HasDupesDistinct(List<int> list)
        {
            return list.Distinct().Count() != list.Count;
        }

        public static bool HasDupesGroupBy(List<int> list)
        {
            return list.GroupBy(i => i).Any(g => g.Count() > 1);
        }

        public static List<int> CreateLargeList(int listSize)
        {
            var list = new List<int>();
            for (var i = 0; i < listSize; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }
}
