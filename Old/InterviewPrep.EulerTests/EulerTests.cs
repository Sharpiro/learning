using InterviewPrep.Euler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace InterviewPrep.EulerTests
{
    [TestClass]
    public class EulerTests
    {
        [TestMethod]
        public void Question1Test()
        {
            var answer = Question1.Compute(10, 3, 5);
            Assert.AreEqual(answer, 23);
            answer = Question1.Compute(1000, 3, 5);
            Assert.AreEqual(answer, 233168);
        }

        [TestMethod]
        public void Question2Test()
        {
            var answer = Question2.Compute();
            Assert.AreEqual(answer, 4613732);
        }

        [TestMethod]
        public void Question3Test()
        {
            var answer = Question3.Compute(13195);
            Assert.AreEqual(answer, 29);
            //todo: too slow doesn't finish
            //answer = Question3.Compute(600851475143);
            //Assert.AreEqual(answer, 6857);
        }

        [TestMethod]
        public void Question4Test()
        {
            var answer = Question4.Compute(2);
            Assert.AreEqual(answer, 9009);
            answer = Question4.Compute(3);
            Assert.AreEqual(answer, 906609);
        }
    }
}
