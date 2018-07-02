using InterviewPrep.Core.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace InterviewPrep.CoreTests.Lists
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            var queue = new CustomQueue();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var value = queue.Dequeue();
            Assert.AreEqual(value, 1);
            Assert.AreEqual(queue.Length, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DequeueEmptyTest()
        {
            var queue = new CustomQueue();
            queue.Enqueue(1);
            queue.Enqueue(2);
            var value = queue.Dequeue();
            var value2 = queue.Dequeue();
            Assert.AreEqual(value, 1);
            Assert.AreEqual(value2, 2);
            queue.Dequeue();
        }

        [TestMethod]
        public void Simple2Test()
        {
            var queue = new CustomQueue();
            for (var i = 0; i < 50; i++)
            {
                queue.Enqueue(i);
            }
            var value = queue.Dequeue();
            Assert.AreEqual(value, 0);
            Assert.AreEqual(49, queue.Length);
        }

        [TestMethod]
        public void LengthTest()
        {
            var queue = new CustomQueue();
            for (var i = 0; i < 50; i++)
            {
                queue.Enqueue(i);
            }
            var list = queue.ToList();
            Assert.AreEqual(50, queue.Length);
            Assert.AreEqual(50, list.Count);
        }
    }
}
