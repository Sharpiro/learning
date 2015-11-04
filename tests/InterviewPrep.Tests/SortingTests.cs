using System.Linq;
using InterviewPrep.Core.Sorting;
using Xunit;

namespace InterviewPrep.Tests
{
    public class SortingTests
    {
        [Fact]
        public void QuickSortTest()
        {
            int[] unsorted1 = { 3, 7, 8, 5, 2, 1, 9, 5, 4 };
            //int[] unsorted2 = { 1, 3, 9, 8, 2, 7, 5 };
            //int[] unsorted3 = { 1, 3, 9, 8, 2, 2, 2, 9, 7, 5, 2 };
            var sorted1 = Sorting.QuickSortEasy(unsorted1, 0, unsorted1.Length - 1);
            //var sorted2 = Sorting.QuickSortEasy(unsorted2, 0, unsorted2.Length - 1);
            //var sorted3 = Sorting.QuickSortEasy(unsorted3, 0, unsorted3.Length - 1);
            Assert.Equal(sorted1, unsorted1.OrderBy(i => i));
            //Assert.Equal(sorted2, unsorted2.OrderBy(i => i));
            //Assert.Equal(sorted3, unsorted3.OrderBy(i => i));
        }
    }
}
