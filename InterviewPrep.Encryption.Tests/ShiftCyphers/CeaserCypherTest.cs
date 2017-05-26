using InterviewPrep.Encryption.Cyphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Encryption.Tests.ShiftCyphers
{
    [TestClass]
    public class CeaserCypherTest
    {
        [TestMethod]
        public void CeaserCypherTest1()
        {
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 19), 19), "david sharp".ToUpper());
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 11), 11), "david sharp".ToUpper());
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 6), 6), "david sharp".ToUpper());
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 4), 4), "david sharp".ToUpper());
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 2), 2), "david sharp".ToUpper());
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 25), 25), "david sharp".ToUpper());
            Assert.AreEqual(CeaserCypherOld.DecryptString(CeaserCypherOld.EncryptString("david sharp", 21), 21), "david sharp".ToUpper());

        }
    }
}
