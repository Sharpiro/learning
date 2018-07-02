using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.CoreTests.Graphs
{
    [TestClass]
    public class GraphExtensionsTest
    {
        [TestMethod]
        public void RemoveMinTest()
        {
            var list = new List<int> { 5, 3, 1, 7, 1, 9 };
            var min = list.RemoveMin();

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(1, min);

            min = list.RemoveMin();

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(1, min);

            min = list.RemoveMin();

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(3, min);
        }
    }
}
