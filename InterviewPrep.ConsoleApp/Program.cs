using System;
using System.Collections.Generic;
using System.IO;
using InterviewPrep.Core.Time;
using InterviewPrep.DailyProgrammer;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            //var path = $"{Directory.GetCurrentDirectory()}\\files\\wordsquaresdict.txt";
            //var helper = new WordSquaresHelper(path);
            //const string input = "eeeeddoonnnsssrv";
            //helper.Find(input);
            var numbers = new List<int> { 8, 1, 6, 3, 5, 7, 4, 9, 2 };
            var numbers2 = new List<int> { 2, 7, 6, 9, 5, 1, 4, 3, 8 };
            var numbers3 = new List<int> { 3, 5, 7, 8, 1, 6, 4, 9, 2 };
            var numbers4 = new List<int> { 8, 1, 6, 7, 5, 3, 4, 9, 2 };
            var numbers5 = new List<int> { 8, 1, 6, 3, 5, 7 };
            var numbers6 = new List<int> { 3, 5, 7, 8, 1, 6 };
            var helper = new MagicSquaresHelper261();
            helper.Solve(numbers);
            helper.Solve(numbers2);
            helper.Solve(numbers3);
            helper.Solve(numbers4);
            helper.Solve(numbers5);
            helper.Solve(numbers6);
            Console.ReadLine();
        }
    }
}