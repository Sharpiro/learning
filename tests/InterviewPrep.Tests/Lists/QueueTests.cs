using System.Collections.Generic;
using InterviewPrep.Core.Lists.SingleLinkedList;
using Xunit;

namespace InterviewPrep.Tests.Lists
{
    public class QueueTests
    {
        [Fact]
        public void QueueSllTest()
        {
            var queue = new QueueSll<int>();
            queue.Enqueue(0);
            queue.Enqueue(1);
            var value = queue.Dequeue();
            Assert.Equal(value, 0);
        }
    }
}
