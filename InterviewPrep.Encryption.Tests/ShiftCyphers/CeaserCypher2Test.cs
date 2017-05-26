using InterviewPrep.Encryption.Cyphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Encryption.Tests.ShiftCyphers
{
    [TestClass]
    public class CeaserCypher2Test
    {
        [TestMethod]
        public void CeaserCypher2Test1()
        {
            const string lowerInput = "david sharp";
            const string mixedInput = "DAVID sharp";

            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(lowerInput, 19), 19), lowerInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(mixedInput, 19), 19), mixedInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(lowerInput, 11), 11), lowerInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(lowerInput, 6), 6), lowerInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(lowerInput, 4), 4), lowerInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(mixedInput, 2), 2), mixedInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(mixedInput, 25), 25), mixedInput);
            Assert.AreEqual(Ceaser.DecryptString(Ceaser.EncryptString(mixedInput, 21), 21), mixedInput);
        }

        [TestMethod]
        public void JakeMessageTest()
        {
            const string expectedPlainText = "Oh hi there jake how are you doing today good fellow how is the life of a spreadsheet ace";
            const string expectedCryptoText = "Ha ab maxkx ctdx ahp tkx rhn whbgz mhwtr zhhw yxeehp ahp bl max ebyx hy t likxtwlaxxm tvx";

            var actualCryptoText = Ceaser.EncryptString(expectedPlainText, 19);
            var actualPlainText = Ceaser.DecryptString(expectedCryptoText, 19);

            Assert.AreEqual(expectedCryptoText, actualCryptoText);
            Assert.AreEqual(expectedPlainText, actualPlainText);
        }

        [TestMethod]
        public void NathanMessageTest()
        {
            Assert.AreEqual(Ceaser.EncryptString("HEY THERE BUDDY BOY NATE SURE HOPE SCHOOL IS DOING REAL FINE AND YOUR MEETING MANY FINE BITTIES", 25)
                , "GDX SGDQD ATCCX ANX MZSD RTQD GNOD RBGNNK HR CNHMF QDZK EHMD ZMC XNTQ LDDSHMF LZMX EHMD AHSSHDR");
            Assert.AreEqual(Ceaser.DecryptString("GDX SGDQD ATCCX ANX MZSD RTQD GNOD RBGNNK HR CNHMF QDZK EHMD ZMC XNTQ LDDSHMF LZMX EHMD AHSSHDR", 25)
                , "HEY THERE BUDDY BOY NATE SURE HOPE SCHOOL IS DOING REAL FINE AND YOUR MEETING MANY FINE BITTIES");
        }//GDX SGDQD ATCCX ANX MZSD RTQD GNOD RBGNNK HR CNHMF QDZK EHMD ZMC XNTQ LDDSHMF LZMX EHMD AHSSHDR
    }
}