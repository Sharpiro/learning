using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewPrep.Core.Lists.SingleLinkedList;
using System.Linq;

namespace InterviewPrep.CoreTests.Lists
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void AddFirstTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddFirst(50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(50, first.Value);
            Assert.AreEqual(1, second.Value);
            Assert.AreEqual(2, third.Value);
            Assert.AreEqual(3, fourth.Value);
        }

        [TestMethod]
        public void AddLastTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Value);
            Assert.AreEqual(2, second.Value);
            Assert.AreEqual(3, third.Value);
            Assert.AreEqual(50, fourth.Value);
        }

        [TestMethod]
        public void AddBeforeTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            var node = list.AddLast(3);
            list.AddBefore(node, 50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Value);
            Assert.AreEqual(2, second.Value);
            Assert.AreEqual(50, third.Value);
            Assert.AreEqual(3, fourth.Value);
        }

        [TestMethod]
        public void AddAfterTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddAfter(node, 50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Value);
            Assert.AreEqual(2, second.Value);
            Assert.AreEqual(50, third.Value);
            Assert.AreEqual(3, fourth.Value);
        }

        [TestMethod]
        public void FindTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            var first = list.Find(3); ;
            Assert.AreEqual(3, first.Value);
        }

        [TestMethod]
        public void RemoveFirstTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddAfter(node, 50);
            list.RemoveFirst();
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(2, first.Value);
            Assert.AreEqual(50, second.Value);
            Assert.AreEqual(3, third.Value);
            Assert.AreEqual(null, fourth);
        }

        [TestMethod]
        public void RemoveLastTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddAfter(node, 50);
            list.RemoveLast();
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Value);
            Assert.AreEqual(2, second.Value);
            Assert.AreEqual(50, third.Value);
            Assert.AreEqual(null, fourth);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new SimpleLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            list.Remove(node);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Value);
            Assert.AreEqual(3, second.Value);
            Assert.AreEqual(50, third.Value);
            Assert.AreEqual(null, fourth);
        }

        [TestMethod]
        public void ToListTest()
        {
            var list = new SimpleLinkedList();
            for (var i = 3; i >= 1; i--)
            {
                list.AddLast(i);
            }
            var standardList = list.ToList();
            standardList.Sort();
            Assert.AreEqual(1, standardList[0]);
            Assert.AreEqual(2, standardList[1]);
            Assert.AreEqual(3, standardList[2]);
            Assert.AreEqual(standardList.Count, list.Count);
        }
    }
}