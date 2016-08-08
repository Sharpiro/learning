using System.Collections.Generic;

namespace InterviewPrep.LinqFundamentals
{
    public static class LinqExtensions
    {
        public static int CountX<T>(this IEnumerable<T> list)
        {
            var counter = 0;
            foreach (var item in list)
            {
                counter++;
            }
            return counter;
        }
    }
}
