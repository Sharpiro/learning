using System;
using static InterviewPrep.Encryption.UnicodeConstants;
using static InterviewPrep.Encryption.Extensions.MathExtension;

namespace InterviewPrep.Encryption.Cyphers
{
    public static class Ceaser
    {
        public static string EncryptString(string inputString, int key)
        {
            if (string.IsNullOrEmpty(inputString)) throw new ArgumentNullException(nameof(inputString));

            var outputChars = new char[inputString.Length];
            for (var i = 0; i < inputString.Length; i++)
            {
                var character = inputString[i];
                var unicodeDelta = char.IsUpper(character) ? UpperDelta : LowerDelta;
                var alphabetPosition = character - unicodeDelta;
                if (character == Space)
                    outputChars[i] = Space;
                else
                    outputChars[i] = (char)(Modulus(alphabetPosition + key, AlphabetLength) + unicodeDelta);
            }
            return new string(outputChars);
        }

        public static string DecryptString(string inputString, int key)
        {
            if (string.IsNullOrEmpty(inputString)) throw new ArgumentNullException(nameof(inputString));

            var outputChars = new char[inputString.Length];
            for (var i = 0; i < inputString.Length; i++)
            {
                var character = inputString[i];
                var unicodeDelta = char.IsUpper(character) ? UpperDelta : LowerDelta;
                var alphabetPosition = character - unicodeDelta;
                if (character == Space)
                    outputChars[i] = Space;
                else
                    outputChars[i] = (char)(Modulus(alphabetPosition - key, AlphabetLength) + unicodeDelta);
            }
            return new string(outputChars);
        }
    }
}