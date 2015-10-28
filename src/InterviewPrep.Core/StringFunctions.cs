using System;
using System.Linq;

namespace InterviewPrep.Core
{
    public static class StringFunctions
    {
        public static string ReverseLinq(this string inputString)
        {
            var reversedCharacters = inputString.Reverse().ToArray();
            var reversedString = new string(reversedCharacters);
            return reversedString;
        }

        public static string ReverseManual(this string inputString)
        {
            var characters = inputString.ToArray();
            var reversedCharacters = new char[characters.Length];
            for (int i = characters.Length - 1, j = 0; i >= 0; i--, j++)
            {
                reversedCharacters[j] = characters[i];
            }
            var reversedString = new string(reversedCharacters);
            return reversedString;
        }
    }
}
