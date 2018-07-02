using System;
using System.Collections.Generic;
using System.Linq;
using InterviewPrep.Encryption.Extensions;

namespace InterviewPrep.Encryption.Cyphers
{
    public class Vigenere
    {
        /// <summary>
        /// Encrypts a string by taking in a string message and a string key.
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="stringKey"></param>
        /// <returns></returns>
        public static string EncryptString(string inputString, string stringKey)
        {
            var spacePositionList = new List<Int32>();
            var outputString = String.Empty;
            var charString = inputString.ToUpper().ToList();
            var key = stringKey.ToUpper().ToCharArray();

            for (var i = 0; i < charString.Count; i++)
            {
                if (charString[i] == UnicodeConstants.Space)
                {
                    spacePositionList.Add(i);
                    charString.RemoveAt(i);
                }
                var keyCharacter = key[MathExtension.Modulus(i, key.Length)] - UnicodeConstants.UpperDelta;
                var alphabetCharacter = charString[i] - UnicodeConstants.UpperDelta;
                var encryptedCharacter = MathExtension.Modulus(alphabetCharacter + keyCharacter, UnicodeConstants.AlphabetLength) + UnicodeConstants.UpperDelta;
                outputString += (char)encryptedCharacter;
            }
            outputString = outputString.AddSpacesToString(spacePositionList);
            return outputString;
        }

        /// <summary>
        /// Decrypts a string by taking in a string crypto message and a string key
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="stringKey"></param>
        /// <returns></returns>
        public static string DecryptString(string inputString, string stringKey)
        {
            var spacePositionList = new List<Int32>();
            var outputString = String.Empty;
            var charString = inputString.ToUpper().ToList();
            var key = stringKey.ToUpper().ToCharArray();

            for (var i = 0; i < charString.Count; i++)
            {
                if (charString[i] == 32)
                {
                    spacePositionList.Add(i);
                    charString.RemoveAt(i);
                }
                var keyCharacter = key[MathExtension.Modulus(i, key.Length)] - UnicodeConstants.UpperDelta;
                var alphabetCharacter = charString[i] - UnicodeConstants.UpperDelta;
                var encryptedCharacter = MathExtension.Modulus(alphabetCharacter - keyCharacter, UnicodeConstants.AlphabetLength) + UnicodeConstants.UpperDelta;
                outputString += (char)encryptedCharacter;
            }
            outputString = outputString.AddSpacesToString(spacePositionList);
            return outputString;
        }
    }
}
