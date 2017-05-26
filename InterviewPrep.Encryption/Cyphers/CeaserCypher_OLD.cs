using System;
using InterviewPrep.Encryption.Extensions;

namespace InterviewPrep.Encryption.Cyphers
{
    public class CeaserCypherOld
    {
        public static string EncryptString(string inputString, int key)
        {
            var outputString = String.Empty;
            foreach (var character in inputString)
            {
                if ((int)character == (int)Alphabet.Space)
                {
                    outputString += character;
                }
                else
                {
                    Alphabet enumValue = character.GetEnumChar();
                    var newIntValue = MathExtension.Modulus(((int)enumValue + key), 26);
                    enumValue = newIntValue.GetEnumChar();
                    outputString += enumValue.ToString();
                    //Console.WriteLine("String: {0}, Int: {1}", enumValue.ToString(), (int)enumValue);
                }
            }
            Console.WriteLine(outputString);
            return outputString;
        }
        public static string DecryptString(string inputString, int key)
        {
            var outputString = String.Empty;
            foreach (var character in inputString)
            {
                if ((int)character == (int)Alphabet.Space)
                {
                    outputString += character;
                }
                else
                {
                    Alphabet enumValue = character.GetEnumChar();
                    var newIntValue = MathExtension.Modulus(((int)enumValue - key), 26);
                    enumValue = newIntValue.GetEnumChar();
                    outputString += enumValue.ToString();
                    //Console.WriteLine("String: {0}, Int: {1}", enumValue.ToString(), (int)enumValue);
                }
            }
            Console.WriteLine(outputString);
            return outputString;
        }
    }
}