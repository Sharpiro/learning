using System;
using System.Buffers;
using Xunit;
using System.Linq;

namespace contiguous_memory
{
    public class ReadOnlySequenceTests
    {
        [Fact]
        public void TestTest()
        {
            var array1 = new[] { 1, 2, 3 };
            var array2 = new[] { 4, 5, 6 };
            var segment1 = new SimpleSegment<int>(array1);
            var segment2 = segment1.Add(array2);
            var sequence = new ReadOnlySequence<int>(segment1, startIndex: 0, segment2, endIndex: 3);
            var sequenceSlice = sequence.Slice(1, 3);

            Assert.True(new[] { 1, 2, 3, 4, 5, 6 }.SequenceEqual(sequence.ToArray()));
            Assert.True(new[] { 2, 3, 4 }.SequenceEqual(sequenceSlice.ToArray()));
        }

        private class SimpleSegment<T> : ReadOnlySequenceSegment<T>
        {
            public SimpleSegment(ReadOnlyMemory<T> memory) => Memory = memory;

            public SimpleSegment<T> Add(ReadOnlyMemory<T> mem)
            {
                var segment = new SimpleSegment<T>(mem);
                segment.RunningIndex = RunningIndex + Memory.Length;
                Next = segment;
                return segment;
            }
        }
    }
}
