using InterviewPrep.Encryption.Cyphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Encryption.Tests.ShiftCyphers
{
    [TestClass]
    public class VigenereCypherTest
    {
        [TestMethod]
        public void VigenereEncryptStringTest()
        {
            Assert.AreEqual(Vigenere.EncryptString("DAVE", "HI"), "KICM");
            Assert.AreEqual(Vigenere.EncryptString("hellogoodfriendhowareyoudoingtodaygoodsir", "TWIZZLER"), "AATKNRSFWBZHDYHYHSIQDJSLWKQMFESUTUONNOWZK");
            Assert.AreEqual(Vigenere.EncryptString("hello there this is a test message", "CDKFE"), "JHVQS VKOWI VKSX MU D DJWV POXWCJO");
        }

        [TestMethod]
        public void VigenereDecryptStringTest()
        {
            Assert.AreEqual(Vigenere.DecryptString("KICM", "HI"), "DAVE");
            Assert.AreEqual(Vigenere.DecryptString("AATKNRSFWBZHDYHYHSIQDJSLWKQMFESUTUONNOWZK", "twizzler"), "HELLOGOODFRIENDHOWAREYOUDOINGTODAYGOODSIR");
            Assert.AreEqual(Vigenere.DecryptString("OQ KICM", "HI"), "HI DAVE");
        }
    }
}
