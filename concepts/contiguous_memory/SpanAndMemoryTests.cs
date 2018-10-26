using System;
using Xunit;

namespace contiguous_memory
{
    // https://msdn.microsoft.com/en-us/magazine/mt814808.aspx?f=255&MSPPError=-2147217396
    // https://www.codemag.com/Article/1807051/Introducing-.NET-Core-2.1-Flagship-Types-Span-T-and-Memory-T
    public class SpanAndMemoryTests
    {
        [Fact]
        public void SpanRangeTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                const string data = "hello world";
                data.AsSpan(0, data.Length + 1);
            });
        }

        [Fact]
        public void ImmutableReverseNoAllocUnsafeTest()
        {
            var data = new int[] { 1, 2, 3, 4, 5 };
            unsafe
            {
                int* ptr = stackalloc int[data.Length];
                var span = new Span<int>(ptr, data.Length);
                data.CopyTo(span);
                span.Reverse();
                Assert.Equal(data[data.Length - 1], span[0]);
            }
        }

        [Fact]
        public void ImmutableReverseNoAllocSafeTest()
        {
            var data = new int[] { 1, 2, 3, 4, 5 };
            var span = data.Length < 128 ? stackalloc int[data.Length] : new int[data.Length];
            data.CopyTo(span);
            span.Reverse();
            Assert.Equal(data[data.Length - 1], span[0]);
        }

        [Fact]
        public void UseSlicesOnSpanRatherThanOnMemry()
        {
            var memory = new Memory<byte>();
            var slice = memory.Span.Slice(0, 0); // good
            var slice2 = memory.Slice(0, 0).Span; // bad
        }


        [Fact]
        public void UseAsSpanOverAsSpanDotSlice()
        {
            var rawData = "data";
            var goodSpan = rawData.AsSpan(0, 1); // good
            var badSpan = rawData.AsSpan().Slice(0, 1); // bad
        }
    }
}
