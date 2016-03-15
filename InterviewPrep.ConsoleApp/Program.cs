using System;
using System.IO;
using InterviewPrep.DailyProgrammer;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\files\\wordsquaresdict.txt";
            var helper = new WordSquaresHelper(path);
            const string input = "4 eeeeddoonnnsssrv";
            helper.Find(input);
            Console.ReadLine();
        }
    }
}