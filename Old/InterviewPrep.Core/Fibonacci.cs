namespace InterviewPrep.Core
{
    public static class Fibonacci
    {
        public static int FibRecursive(int n)
        {
            if (n < 2)
                return n;

            return FibRecursive(n - 2) + FibRecursive(n - 1);
        }

        public static int FibIterative(int n)
        {
            var fibI = 0;
            var fibIMinusOne = 0;
            var fibIMinusTwo = 0;

            for (var i = 0; i <= n; i++)
            {
                fibI = i < 2 ? i : fibIMinusOne + fibIMinusTwo;
                fibIMinusTwo = fibIMinusOne;
                fibIMinusOne = fibI;
            }
            return fibI;
        }
    }
}