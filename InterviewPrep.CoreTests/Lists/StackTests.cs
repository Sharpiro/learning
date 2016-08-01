using InterviewPrep.Core.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace InterviewPrep.CoreTests.Lists
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void SimpleTest()
        {
            var stack = new CustomStack();
            stack.Push(1);
            stack.Push(2);
            var value = stack.Pop();
            var value2 = stack.Pop();
            Assert.AreEqual(value, 2);
            Assert.AreEqual(value2, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PopEmptyTest()
        {
            var stack = new CustomStack();
            stack.Push(1);
            stack.Push(2);
            var value = stack.Pop();
            var value2 = stack.Pop();
            Assert.AreEqual(value2, 1);
            Assert.AreEqual(value2, 1);
            stack.Pop();
        }

        [TestMethod]
        public void Simple2Test()
        {
            var stack = new CustomStack();
            for (var i = 0; i < 50; i++)
            {
                stack.Push(i);
            }
            var value = stack.Pop();
            Assert.AreEqual(value, 49);
            Assert.AreEqual(49, stack.Length);
        }

        [TestMethod]
        public void LengthTest()
        {
            var stack = new CustomStack();
            for (var i = 0; i < 50; i++)
            {
                stack.Push(i);
            }
            var list = stack.ToList();
            Assert.AreEqual(50, stack.Length);
            Assert.AreEqual(50, list.Count);
        }
    }
}
