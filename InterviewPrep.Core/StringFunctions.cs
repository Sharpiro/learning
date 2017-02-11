using System;
using System.Linq;
using System.Text;

namespace InterviewPrep.Core
{
    public static class StringFunctions
    {
        public static string ReverseLinq(this string inputString)
        {
            var chars = inputString.Reverse();
            return new string(chars.ToArray());
        }

        public static string ReverseStringBuilder(this string inputString)
        {
            var builder = new StringBuilder();
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                builder.Append(inputString[i]);
            }
            return builder.ToString();
        }

        public static string ReverseWordInString(this string inputString, string word)
        {
            var index = inputString.IndexOf(word);
            var charArray = inputString.ToArray();
            for (int i = word.Length + index - 1, j = 0; i >= index; i--, j++)
            {
                charArray[i] = word[j];
            }

            return new string(charArray);
        }

        public static string ReverseWordInStringMany(this string inputString, string word)
        {
            var index = 0;
            var currentString = inputString;
            while ((index = currentString.IndexOf(word)) != -1)
            {
                currentString = ReverseWordInString(currentString, word);
            }
            return currentString;
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

        public static bool IsAnagram(this string x, string y)
        {
            if (x.Length != y.Length)
                return false;

            var sortedX = x.OrderBy(s => s).ToList();
            var sortedY = y.OrderBy(s => s).ToList();

            for (var i = 0; i < sortedX.Count(); i++)
            {
                if (sortedX[i] != sortedY[i])
                    return false;
            }

            return true;
        }
    }
}
