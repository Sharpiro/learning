namespace InterviewPrep.Core
{
    public static class Fibonacci
    {
        public static int FibRecursive(int n)
        {
            if (n < 2)
                return n;
            return FibRecursive(n - 1) + FibRecursive(n - 2);
        }


        public static int FibIterative(int n)
        {
            var nMinus2 = 0;
            var nMinus1 = 0;
            var currentValue = 0;
            for (var i = 0; i <= n; i++)
            {
                if (i < 2)
                {
                    currentValue = i;
                }
                else
                {
                    currentValue = nMinus1 + nMinus2;
                }
                nMinus2 = nMinus1;
                nMinus1 = currentValue;
            }
            return currentValue;
        }
    }
}
