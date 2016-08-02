using Interview.LinqFundamentals;
using Xunit;

namespace InterviewPrep.Pluralsight.Core.Tests
{
    public class FileHelperTests
    {
        public FileHelperTests()
        {
            
        }
        [Fact]
        public void TestThing()
        {
            var directory = "c:/windows";
            var helper = new FileHelper();
            helper.GetLargestFile(directory);
        }
    }
}
