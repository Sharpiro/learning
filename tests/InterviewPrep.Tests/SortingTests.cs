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
            int[] exampleList = { 3, 7, 8, 5, 2, 1, 9, 5, 4 };
            //int[] exampleList = { 1, 3, 9, 8, 2, 7, 5 };
            var exampleListOrdered = exampleList.OrderBy(i => i);
            var sorted = Sorting.QuickSortEasy(exampleList, 0, exampleList.Length - 1);
            Assert.Equal(sorted, exampleListOrdered);
        }
    }
}
