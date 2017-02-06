using InterviewPrep.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class FizzBuzzWoofTests
    {
        [TestMethod]
        public void PlayTest()
        {
            const string expected = "1, 2, Fizz, 4, Buzz, Fizz, Woof";
            var game = new FizzBuzzWoof("1, 2, 3, 4, 5, 6, 7");
            var actual = game.Play();

            Assert.AreEqual(expected, actual);
        }
    }
}