using System;
using System.Linq;
using System.Text;

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
            var reversedStringX = new StringBuilder();
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                reversedStringX.Append(inputString[i]);
            }
            return reversedStringX.ToString();
        }

        public static string ReverseWordInString(this string inputString, string word)
        {
            var charArray = inputString.ToCharArray();
            int wordIndex;
            while ((wordIndex = inputString.IndexOf(word, StringComparison.Ordinal)) != -1)
            {
                for (int i = wordIndex, j = word.Length - 1; i < wordIndex + word.Length; i++, j--)
                {
                    charArray[i] = word[j];
                }
                inputString = new string(charArray);
            }
            return inputString;
        }

        public static string ReverseWordInStringBetter(this string inputString, string word)
        {
            var wordMatch = 0;
            var newString = new StringBuilder();
            var lookaheadMatch = false;
            for (int i = 0, reversed = word.Length - 1; i < inputString.Length; i++, reversed--)
            {
                if (!lookaheadMatch)
                {
                    for (int j = 0, k = i; j < word.Length && k < inputString.Length; j++, k++)
                    {
                        if (inputString[k].Equals(word[j]))
                        {
                            wordMatch++;
                            if (wordMatch == word.Length)
                            {
                                reversed = word.Length - 1;
                                lookaheadMatch = true;
                                wordMatch = 0;
                            }
                        }
                        else
                            break;
                    }
                }
                if (lookaheadMatch)
                {
                    if (reversed >= 0)
                        newString.Append(word[reversed]);
                    else
                    {
                        newString.Append(inputString[i]);
                        lookaheadMatch = false;
                    }
                }
                else
                    newString.Append(inputString[i]);
            }
            return newString.ToString();
        }
    }
}
