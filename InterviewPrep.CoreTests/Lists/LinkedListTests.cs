using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewPrep.Core.Lists;
using System.Linq;

namespace InterviewPrep.CoreTests.Lists
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void AddLastTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Data);
            Assert.AreEqual(2, second.Data);
            Assert.AreEqual(3, third.Data);
            Assert.AreEqual(50, fourth.Data);
        }

        [TestMethod]
        public void AddFirstTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddFirst(50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(50, first.Data);
            Assert.AreEqual(1, second.Data);
            Assert.AreEqual(2, third.Data);
            Assert.AreEqual(3, fourth.Data);
        }

        [TestMethod]
        public void AddBeforeTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            var node = list.AddLast(3);
            list.AddBefore(node, 50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Data);
            Assert.AreEqual(2, second.Data);
            Assert.AreEqual(50, third.Data);
            Assert.AreEqual(3, fourth.Data);
        }

        [TestMethod]
        public void AddBeforeHeadTest()
        {
            var list = new ComplexLinkedList();
            var node = list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddBefore(node, 50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(50, first.Data);
            Assert.AreEqual(1, second.Data);
            Assert.AreEqual(2, third.Data);
            Assert.AreEqual(3, fourth.Data);
        }

        [TestMethod]
        public void AddAfterTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddAfter(node, 50);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Data);
            Assert.AreEqual(2, second.Data);
            Assert.AreEqual(50, third.Data);
            Assert.AreEqual(3, fourth.Data);
        }

        [TestMethod]
        public void FindTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            var first = list.Find(3); ;
            Assert.AreEqual(3, first.Data);
        }

        [TestMethod]
        public void FindNothingTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            var first = list.Find(12); ;
            Assert.AreEqual(null, first);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddLast(50);
            list.Remove(node);
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(1, first.Data);
            Assert.AreEqual(3, second.Data);
            Assert.AreEqual(50, third.Data);
            Assert.AreEqual(null, fourth);
        }

        [TestMethod]
        public void RemoveFirstTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            var node = list.AddLast(2);
            list.AddLast(3);
            list.AddAfter(node, 50);
            list.RemoveFirst();
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            var fourth = third.Next;
            Assert.AreEqual(2, first.Data);
            Assert.AreEqual(50, second.Data);
            Assert.AreEqual(3, third.Data);
            Assert.AreEqual(null, fourth);
        }

        [TestMethod]
        public void RemoveLastTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.RemoveLast();
            var first = list.First;
            var second = first.Next;
            var third = second.Next;
            Assert.AreEqual(1, first.Data);
            Assert.AreEqual(2, second.Data);
            Assert.AreEqual(null, third);
        }

        [TestMethod]
        public void RemoveLastItemTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.RemoveLast();
            var first = list.First;
            Assert.AreEqual(null, first);
        }

        [TestMethod]
        public void RemoveHeadTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(4);
            list.AddLast(8);
            list.AddLast(2);
            list.RemoveFirst();
            Assert.AreEqual(2, list.First.Data);
            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void CountTest()
        {
            var list = new ComplexLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(3);
            list.AddLast(3);
            Assert.AreEqual(5, list.Count);
            list.RemoveFirst();
            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void ToListTest()
        {
            var list = new ComplexLinkedList();
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

        [TestMethod]
        public void FindNodePositionTest()
        {
            var list = new ComplexLinkedList();
            for (var i = 0; i < 10; i++)
            {
                list.AddLast(i);
            }
            var standardList = list.ToList();
            var node = list.Find(5); ;
            var pos = list.FindPos(node);
            Assert.AreEqual(5, standardList[5]);
            Assert.AreEqual(5, pos);
        }
    }
}