using System.Collections.Generic;
using InterviewPrep.Core;
using Xunit;

namespace InterviewPrep.Tests
{
    public class SearcherTests
    {
        [Fact]
        public void LinearSearchTest()
        {
            var list = new List<int> { 1, 3, 5, 8, 2, 4, 7, 6, 4, 3 };
            int index;
            var result1 = Searcher.LinearSearch(list, 8, out index);
            Assert.Equal(index, 3);
            var result2 = Searcher.LinearSearch(list, 3, out index);
            Assert.Equal(index, 1);
            var result3 = Searcher.LinearSearch(list, 4, out index);
            Assert.Equal(index, 5);
            var result4 = Searcher.LinearSearch(list, 12, out index);
            Assert.Equal(index, -1);
            Assert.Equal(result1, 8);
            Assert.Equal(result2, 3);
            Assert.Equal(result3, 4);
            Assert.Equal(result4, 1);
        }

        [Fact]
        public void BinarySearchTest()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result1 = Searcher.BinarySearch(list, 8);
            var result2 = Searcher.BinarySearch(list, 3);
            var result3 = Searcher.BinarySearch(list, 4);
            var result4 = Searcher.BinarySearch(list, 12);
            Assert.Equal(result1, 8);
            Assert.Equal(result2, 3);
            Assert.Equal(result3, 4);
            Assert.Equal(result4, -1);
        }

        [Fact]
        public void BinarySearchRecursiveTest()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result1 = Searcher.BinarySearchRecursive(list, 8, 0, list.Count - 1);
            var result2 = Searcher.BinarySearchRecursive(list, 3, 0, list.Count - 1);
            var result3 = Searcher.BinarySearchRecursive(list, 4, 0, list.Count - 1);
            var result4 = Searcher.BinarySearchRecursive(list, 12, 0, list.Count - 1);
            Assert.Equal(result1, 8);
            Assert.Equal(result2, 3);
            Assert.Equal(result3, 4);
            Assert.Equal(result4, -1);
        }
    }
}
