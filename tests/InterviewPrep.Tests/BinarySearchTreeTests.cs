using InterviewPrep.Core.BinaryTrees;
using Xunit;

namespace InterviewPrep.Tests
{
    public class BinarySearchTreeTests
    {
        [Fact]
        public void AddTest()
        {
            var tree = new BinarySearchTree();
            for (var i = 0; i < 10; i++)
            {
                tree.Add(i);
            }
        }
    }
}
