using InterviewPrep.Encryption.Breakers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Encryption.Tests.ShiftCyphers
{
    [TestClass]
    public class CaesarBreakerTests
    {
        [TestMethod]
        public void BreakCaesarTest()
        {
            const string expectedPlainText = "Oh hi there jake how are you doing today good fellow how is the life of a spreadsheet ace";
            const string expectedCryptoText = "Ha ab maxkx ctdx ahp tkx rhn whbgz mhwtr zhhw yxeehp ahp bl max ebyx hy t likxtwlaxxm tvx";

            var breaker = new CeaserBreaker();
            var actualPlaintText = breaker.BreakByVowels(expectedCryptoText);

            Assert.AreEqual(expectedPlainText, actualPlaintText);
        }
    }
}