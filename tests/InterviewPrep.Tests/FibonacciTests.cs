using InterviewPrep.Core;
using Xunit;

namespace InterviewPrep.Tests
{
    public class FibonacciTests
    {
        [Fact]
        public void FibIterativeTest()
        {
            var x1 = Fibonacci.FibIterative(0);
            var x2 = Fibonacci.FibIterative(1);
            var x3 = Fibonacci.FibIterative(2);
            var x4 = Fibonacci.FibIterative(3);
            var x5 = Fibonacci.FibIterative(4);
            var x6 = Fibonacci.FibIterative(5);
            var x7 = Fibonacci.FibIterative(6);
            var x8 = Fibonacci.FibIterative(7);
            var x9 = Fibonacci.FibIterative(8);
            var x10 = Fibonacci.FibIterative(9);
            var x11 = Fibonacci.FibIterative(10);
            var x12 = Fibonacci.FibIterative(25);

            Assert.Equal(x1, 0);
            Assert.Equal(x2, 1);
            Assert.Equal(x3, 1);
            Assert.Equal(x4, 2);
            Assert.Equal(x5, 3);
            Assert.Equal(x6, 5);
            Assert.Equal(x7, 8);
            Assert.Equal(x8, 13);
            Assert.Equal(x9, 21);
            Assert.Equal(x10, 34);
            Assert.Equal(x11, 55);
            Assert.Equal(x12, 75025);
        }

        [Fact]
        public void FibRecursiveTest()
        {
            var x1 = Fibonacci.FibRecursive(0);
            var x2 = Fibonacci.FibRecursive(1);
            var x3 = Fibonacci.FibRecursive(2);
            var x4 = Fibonacci.FibRecursive(3);
            var x5 = Fibonacci.FibRecursive(4);
            var x6 = Fibonacci.FibRecursive(5);
            var x7 = Fibonacci.FibRecursive(6);
            var x8 = Fibonacci.FibRecursive(7);
            var x9 = Fibonacci.FibRecursive(8);
            var x10 = Fibonacci.FibRecursive(9);
            var x11 = Fibonacci.FibRecursive(10);
            var x12 = Fibonacci.FibRecursive(25);

            Assert.Equal(x1, 0);
            Assert.Equal(x2, 1);
            Assert.Equal(x3, 1);
            Assert.Equal(x4, 2);
            Assert.Equal(x5, 3);
            Assert.Equal(x6, 5);
            Assert.Equal(x7, 8);
            Assert.Equal(x8, 13);
            Assert.Equal(x9, 21);
            Assert.Equal(x10, 34);
            Assert.Equal(x11, 55);
            Assert.Equal(x12, 75025);
        }
    }
}
