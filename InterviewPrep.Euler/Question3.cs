using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Euler
{
    //todo: too slow doesn't finish
    /// <summary>
    /// Question 3
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    public static class Question3
    {
        public static long Compute(long largeNumber)
        {
            var primeFactors = new List<long>();
            for (long i = 2; i < largeNumber; i++)
            {
                if (i == 0)
                    throw new Exception();
                if (largeNumber.HasFactor(i) && i.IsPrime())
                {
                    primeFactors.Add(i);
                }
            }
            return primeFactors.OrderByDescending(p => p).FirstOrDefault();
        }

        private static bool HasFactor(this long number, long divisor)
        {
            return number % divisor == 0;
        }

        private static bool IsPrime(this long factor)
        {
            for (var i = 2; i < factor; i++)
            {
                if (factor % i == 0)
                    return false;
            }
            return true;
        }
    }
}
