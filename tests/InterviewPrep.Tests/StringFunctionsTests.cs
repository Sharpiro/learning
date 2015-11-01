using InterviewPrep.Core;
using Xunit;

namespace InterviewPrep.Tests
{
    public class StringFunctionsTests
    {
        [Fact]
        public void ReverseWordInStringBetterTest()
        {
            var forward = "man hello man";
            var reverse = forward.ReverseWordInStringBetter("man");
            Assert.Equal("nam hello nam", reverse);
        }
        
        [Fact]
        public void ReverseWordInSringTest()
        {
            var forward = "hello man";
            var reverse = forward.ReverseWordInString("man");
            Assert.Equal("hello nam", reverse);
        }

        [Fact]
        public void ReverseStringManualTest()
        {
            var forward = "hello david";
            var reverse = forward.ReverseManual();
            Assert.Equal("divad olleh", reverse);
        }

        [Fact]
        public void ReverseStringLinqTest()
        {
            var forward = "hello david";
            var reverse = forward.ReverseLinq();
            Assert.Equal("divad olleh", reverse);
        }
    }
}
