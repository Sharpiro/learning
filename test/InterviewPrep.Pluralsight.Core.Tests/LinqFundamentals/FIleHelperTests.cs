using InterviewPrep.LinqFundamentals;
using Xunit;
using System.Linq;

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
            var largestFilesMethod = helper.GetLargestFileMethodSyntax(directory);
            var largestFilesQuery = helper.GetLargestFileQuerySyntax(directory);
            Assert.Equal(5, largestFilesMethod.Count());
            Assert.Equal(5, largestFilesQuery.Count());
            //Assert.Equal(largestFilesMethod, largestFilesQuery);
        }

        [Fact]
        public void CompareToThing()
        {
            var result = (1).CompareTo(1);
            result = (1).CompareTo(2);
            result = (2).CompareTo(1);
            result = ("hi").CompareTo("there");
            result = ("th").CompareTo("h");
        }
    }
}