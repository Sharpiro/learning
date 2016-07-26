using System.Collections.Generic;

namespace InterviewPrep.Core.Sorting
{
    public interface ISorter
    {
        IEnumerable<int> Sort(IEnumerable<int> list);
    }
}