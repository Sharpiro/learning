using System.Collections.Generic;
using InterviewPrep.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class SearcherTests
    {
        [TestMethod]
        public void LinearSearchTest()
        {
            var list = new List<int> { 1, 3, 5, 8, 2, 4, 7, 6, 4, 3 };
            var result1 = Searcher.LinearSearch(list, 8);
            Assert.AreEqual(result1.Index, 3);
            var result2 = Searcher.LinearSearch(list, 3);
            Assert.AreEqual(result2.Index, 1);
            var result3 = Searcher.LinearSearch(list, 4);
            Assert.AreEqual(result3.Index, 5);
            var result4 = Searcher.LinearSearch(list, 12);
            Assert.AreEqual(result4.Index, -1);
            Assert.AreEqual(result1.Item, 8);
            Assert.AreEqual(result2.Item, 3);
            Assert.AreEqual(result3.Item, 4);
            Assert.AreEqual(result4.Item, 0);
        }

        [TestMethod]
        public void BinarySearchTest()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result1 = Searcher.BinarySearch(list, 8);
            var result2 = Searcher.BinarySearch(list, 3);
            var result3 = Searcher.BinarySearch(list, 4);
            var result4 = Searcher.BinarySearch(list, 12);
            Assert.AreEqual(result1, 8);
            Assert.AreEqual(result2, 3);
            Assert.AreEqual(result3, 4);
            Assert.AreEqual(result4, -1);
        }

        [TestMethod]
        public void BinarySearchRecursiveTest()
        {
            var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result1 = Searcher.BinarySearchRecursive(list, 8, 0, list.Count - 1);
            var result2 = Searcher.BinarySearchRecursive(list, 3, 0, list.Count - 1);
            var result3 = Searcher.BinarySearchRecursive(list, 4, 0, list.Count - 1);
            var result4 = Searcher.BinarySearchRecursive(list, 12, 0, list.Count - 1);
            Assert.AreEqual(result1, 8);
            Assert.AreEqual(result2, 3);
            Assert.AreEqual(result3, 4);
            Assert.AreEqual(result4, -1);
        }
    }
}