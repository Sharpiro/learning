using InterviewPrep.Core.Lists.SingleLinkedList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests.Lists
{
    public class LinkedListTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            Assert.AreEqual(list.Count, 15);
        }

        [TestMethod]
        public void GetTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            var node = list.Get(5);
            Assert.IsNotNull(node);
            Assert.AreEqual(node.Value, 4);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            var node = list.Delete(2);
            Assert.AreEqual(list.Count, 14);
            Assert.AreEqual(node.Value, 1);

            node = list.Delete(5);
            Assert.AreEqual(list.Count, 13);
            Assert.AreEqual(node.Value, 5);
        }

        [TestMethod]
        public void ToListTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            var standardList = list.ToList();
            Assert.AreEqual(standardList.Count, list.Count);
        }
    }
}
