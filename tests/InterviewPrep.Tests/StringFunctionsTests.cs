using InterviewPrep.Core;
using Xunit;

namespace InterviewPrep.Tests
{
    public class StringFunctionsTests
    {
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
