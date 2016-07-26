using System.Linq;
using InterviewPrep.Core.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class SortingTests
    {
        private ISorter _sorter;

        public SortingTests()
        {
            _sorter = new MergeSort();
        }

        [TestMethod]
        public void SmallSortTest1()
        {
            int[] unsorted = { 4, 3, 7 };
            var expected = unsorted.OrderBy(number => number).ToList();
            var actual = _sorter.Sort(unsorted).ToList();
            for (var i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void QuickSortTest1()
        {
            int[] unsorted = { 4, 3, 7, 8, 5, 2, 1, 9, 5, 4 };
            var expected = unsorted.OrderBy(number => number).ToList();
            var actual = _sorter.Sort(unsorted).ToList();
            for (var i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void QuickSortTest2()
        {
            int[] unsorted = { 1, 3, 9, 8, 2, 7, 5 };
            var expected = unsorted.OrderBy(number => number).ToList();
            var actual = _sorter.Sort(unsorted).ToList();
            for (var i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void QuickSortTest3()
        {
            var unsorted = new int[] { 1, 3, 9, 8, 2, 2, 2, 9, 7, 5, 2 };
            var expected = unsorted.OrderBy(number => number).ToList();
            var actual = _sorter.Sort(unsorted).ToList();
            for (var i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void QuickSortTest4()
        {
            var unsorted = new int[] { 12, 7, 14, 9, 10, 11 };
            var expected = unsorted.OrderBy(number => number).ToList();
            var actual = _sorter.Sort(unsorted).ToList();
            for (var i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void QuickSortTest5()
        {
            var unsorted = new int[] { 9, 7, 5, 11, 12, 2, 14, 3, 10, 6 };
            var expected = unsorted.OrderBy(number => number).ToList();
            var actual = _sorter.Sort(unsorted).ToList();
            for (var i = 0; i < unsorted.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
