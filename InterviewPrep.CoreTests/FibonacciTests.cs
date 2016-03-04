using InterviewPrep.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class FibonacciTests
    {
        [TestMethod]
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

            Assert.AreEqual(x1, 0);
            Assert.AreEqual(x2, 1);
            Assert.AreEqual(x3, 1);
            Assert.AreEqual(x4, 2);
            Assert.AreEqual(x5, 3);
            Assert.AreEqual(x6, 5);
            Assert.AreEqual(x7, 8);
            Assert.AreEqual(x8, 13);
            Assert.AreEqual(x9, 21);
            Assert.AreEqual(x10, 34);
            Assert.AreEqual(x11, 55);
            Assert.AreEqual(x12, 75025);
        }

        [TestMethod]
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

            Assert.AreEqual(x1, 0);
            Assert.AreEqual(x2, 1);
            Assert.AreEqual(x3, 1);
            Assert.AreEqual(x4, 2);
            Assert.AreEqual(x5, 3);
            Assert.AreEqual(x6, 5);
            Assert.AreEqual(x7, 8);
            Assert.AreEqual(x8, 13);
            Assert.AreEqual(x9, 21);
            Assert.AreEqual(x10, 34);
            Assert.AreEqual(x11, 55);
            Assert.AreEqual(x12, 75025);
        }
    }
}
