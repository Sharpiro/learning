using System;
using System.Text;

namespace InterviewPrep.Euler
{
    /// <summary>
    /// Question 4
    /// A palindromic number reads the same both ways. T
    /// he largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
    /// Find the largest palindrome made from the product of two 3-digit numbers.
    /// </summary>
    public static class Question4
    {
        public static int Compute(int numberOfDigits)
        {
            var max = Math.Pow(10, numberOfDigits);
            var maxPalindrome = 0;
            for (var i = 0; i < max; i++)
            {
                for (var j = 0; j < max; j++)
                {
                    var product = i * j;
                    if (product.IsPalindrome())
                    {
                        maxPalindrome = product > maxPalindrome ? product : maxPalindrome;
                    }
                }
            }
            return maxPalindrome;
        }

        private static bool IsPalindrome(this int number)
        {
            var forwardString = number.ToString();
            //var reversedCharArray = forwardString.Reverse().ToArray();
            //var reversedString = new string(reversedCharArray);
            var reversedString = forwardString.ReverseManual();
            var isPalindrome = forwardString == reversedString;
            return isPalindrome;
        }

        private static string ReverseManual(this string inputString)
        {
            var builder = new StringBuilder();
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                builder.Append(inputString[i]);
            }
            return builder.ToString();
        }
    }
}
