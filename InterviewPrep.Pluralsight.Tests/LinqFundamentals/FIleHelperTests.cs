using InterviewPrep.LinqFundamentals;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Pluralsight.Core.Tests
{
    [TestClass]
    public class FileHelperTests
    {
        public FileHelperTests()
        {

        }

        [TestMethod]
        public void TestThing()
        {
            var directory = "c:/windows";
            var helper = new FileHelper();
            var largestFilesMethod = helper.GetLargestFileMethodSyntax(directory);
            var largestFilesQuery = helper.GetLargestFileQuerySyntax(directory);
            Assert.AreEqual(5, largestFilesMethod.Count());
            Assert.AreEqual(5, largestFilesQuery.Count());
            //Assert.AreEqual(largestFilesMethod, largestFilesQuery);
        }

        [TestMethod]
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