using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewPrep.Core.Lists.SingleLinkedList;

namespace InterviewPrep.CoreTests.Lists
{
    [TestClass]
    public class LinkedListTests
    {
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
        //    [TestMethod]
        //    public void InsertTest()
        //    {
        //        var list = new SingleLinkedList<int>();
        //        for (var i = 0; i < 15; i++)
        //        {
        //            list.Insert(i);
        //        }
        //        Assert.AreEqual(list.Count, 15);
        //    }

        //    [TestMethod]
        //    public void GetTest()
        //    {
        //        var list = new SingleLinkedList<int>();
        //        for (var i = 0; i < 15; i++)
        //        {
        //            list.Insert(i);
        //        }
        //        var node = list.Get(5);
        //        Assert.IsNotNull(node);
        //        Assert.AreEqual(node.Value, 4);
        //    }

        //    [TestMethod]
        //    public void DeleteTest()
        //    {
        //        var list = new SingleLinkedList<int>();
        //        for (var i = 0; i < 15; i++)
        //        {
        //            list.Insert(i);
        //        }
        //        var node = list.Delete(2);
        //        Assert.AreEqual(list.Count, 14);
        //        Assert.AreEqual(node.Value, 1);

        //        node = list.Delete(5);
        //        Assert.AreEqual(list.Count, 13);
        //        Assert.AreEqual(node.Value, 5);
        //    }

        //    [TestMethod]
        //    public void ToListTest()
        //    {
        //        var list = new SingleLinkedList<int>();
        //        for (var i = 0; i < 15; i++)
        //        {
        //            list.Insert(i);
        //        }
        //        var standardList = list.ToList();
        //        Assert.AreEqual(standardList.Count, list.Count);
        //    }
    }
}
