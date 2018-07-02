namespace InterviewPrep.Euler
{
    /// <summary>
    /// Question 1
    /// If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
    /// Find the sum of all the multiples of 3 or 5 below 1000.
    /// </summary>
    public static class Question1
    {
        public static int Compute(int max, params int[] testNumbers)
        {
            var sum = 0;
            for (var i = 1; i < max; i++)
            {
                foreach (var number in testNumbers)
                {
                    if (i % number != 0) continue;
                    sum += i;
                    break;
                }
            }
            return sum;
        }
    }
}
