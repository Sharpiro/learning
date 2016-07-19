using InterviewPrep.Core;
using System;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var table = new CustomHashTable();
            table.Add(127, 1);
            table.Add(1023, 2);
            var value1 = table.Get(127);
            var value2 = table.Get(1023);
            if (value1 != 1 || value2 != 2)
                throw new Exception();
            Console.ReadLine();
        }
    }
}