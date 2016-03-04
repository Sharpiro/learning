using InterviewPrep.Core.Lists.SingleLinkedList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests.Lists
{
    public class QueueTests
    {
        [TestMethod]
        public void QueueSllTest()
        {
            var queue = new QueueSll<int>();
            queue.Enqueue(0);
            queue.Enqueue(1);
            var value = queue.Dequeue();
            Assert.AreEqual(value, 0);
        }
    }
}
