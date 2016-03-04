using System;

namespace InterviewPrep.Euler
{
    /// <summary>
    /// Question 2
    /// Of the Fib numbers that do not exceed 4 million, find the sum of the even-valued terms
    /// </summary>
    public static class Question2
    {
        public static int Compute()
        {
            int evenSum;
            var fib = FibIterative(int.MaxValue, out evenSum, 4000000);
            return evenSum;
        }

        /// <summary>
        /// Iteratively compiles Fib Sequence
        /// </summary>
        /// <param name="n">The max place to find a Fib number</param>
        /// <param name="evenSum">The sum of all of the even Fib numbers</param>
        /// <param name="maxFib"></param>
        /// <returns></returns>
        private static int FibIterative(int n, out int evenSum, int maxFib = int.MaxValue)
        {
            var nMinus2 = 0;
            var nMinus1 = 0;
            var fib = 0;
            evenSum = 0;
            for (var i = 0; i <= n; i++)
            {
                if (i < 2)
                {
                    fib = i;
                }
                else
                {
                    fib = nMinus2 + nMinus1;
                }
                if (fib % 2 == 0)
                    evenSum += fib;
                if (fib > maxFib)
                    break;
                nMinus2 = nMinus1;
                nMinus1 = fib;
            }
            return fib;
        }
    }
}
