using System.Collections.Generic;
using InterviewPrep.Encryption.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Encryption.Tests.Extensions
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void ModulusTest()
        {
            Assert.AreEqual(6, MathExtension.Modulus(32, 26));
            Assert.AreEqual(86, MathExtension.Modulus(86, 492));
            Assert.AreEqual(7, MathExtension.Modulus(-19, 26));
            Assert.AreEqual(11, MathExtension.Modulus(-15, 26));
            Assert.AreEqual(15, MathExtension.Modulus(-11, 26));
            Assert.AreEqual(19, MathExtension.Modulus(-7, 26));
            Assert.AreEqual(20, MathExtension.Modulus(-32, 26));
            Assert.AreEqual(24, MathExtension.Modulus(-492, 86));
        }

        [TestMethod]
        public void AddSpacesToStringTest()
        {
            const string input = "hellohowareyou";
            const string expectedOutput = "hello how are you";

            var spaces = new List<int> { 5, 8, 11 };

            var actualOutput = input.AddSpacesToString(spaces);

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}