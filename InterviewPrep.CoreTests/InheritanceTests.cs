using InterviewPrep.Core.Inheritance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class InheritanceTests
    {
        [TestMethod]
        public void OverrideAndNewTest()
        {
            Base @base = new Base();
            Base childX = new ChildOne();
            var childY = new ChildOne();

            var baseOverrideResult = @base.GetOverride();
            var childXOverrideResult = childX.GetOverride();
            var childYOverrideResult = childY.GetOverride();
            var baseNewResult = @base.GetNew();
            var childXNewResult = childX.GetNew();
            var childYNewResult = childY.GetNew();

            Assert.AreEqual(0, baseOverrideResult);
            Assert.AreEqual(1, childXOverrideResult);
            Assert.AreEqual(1, childYOverrideResult);
            Assert.AreEqual(0, baseNewResult);
            Assert.AreEqual(0, childXNewResult);
            Assert.AreEqual(1, childYNewResult);
        }
    }
}