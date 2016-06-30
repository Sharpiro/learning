using InterviewPrep.Core.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace InterviewPrep.CoreTests.Compiler
{
    [TestClass]
    public class TokensTests
    {
        [TestMethod]
        public void GetEnumeratorTest()
        {
            var tokens = new Tokens();
            var token = tokens.FirstOrDefault();
            Assert.IsNotNull(token.Value);
            Assert.IsNotNull(token.Type);
        }

        [TestMethod]
        public void ContainsTest()
        {
            var tokens = new Tokens();
            var token = tokens.FirstOrDefault();
            var fakeToken = new Token { Value = ".", Type = TokenType.Symbol };
            var isInList = tokens.Contains(token);
            var isInList2 = tokens.Contains(fakeToken);
            var notInList = tokens.Contains(",");
            var isInList3 = tokens.Contains("public");
            Assert.IsTrue(isInList);
            Assert.IsTrue(isInList2);
            Assert.IsFalse(notInList);
            Assert.IsTrue(isInList3);
        }
    }
}
