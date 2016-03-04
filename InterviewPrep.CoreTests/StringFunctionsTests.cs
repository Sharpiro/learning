using InterviewPrep.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class StringFunctionsTests
    {
        [TestMethod]
        public void ReverseWordInStringBetterTest()
        {
            var forward = "man hello man";
            var reverse = forward.ReverseWordInStringBetter("man");
            Assert.AreEqual("nam hello nam", reverse);
        }
        
        [TestMethod]
        public void ReverseWordInSringTest()
        {
            var forward = "hello man";
            var reverse = forward.ReverseWordInString("man");
            Assert.AreEqual("hello nam", reverse);
        }

        [TestMethod]
        public void ReverseStringManualTest()
        {
            var forward = "hello david";
            var reverse = forward.ReverseManual();
            Assert.AreEqual("divad olleh", reverse);
        }

        [TestMethod]
        public void ReverseStringLinqTest()
        {
            var forward = "hello david";
            var reverse = forward.ReverseLinq();
            Assert.AreEqual("divad olleh", reverse);
        }
    }
}
