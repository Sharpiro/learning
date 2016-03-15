using System;
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
            //const string input = "4 eeeeddoonnnsssrv";
            //helper.Find(input);
            //var time = TimeFunctions.GetRemainingTime(new DateTime(2016, 03, 15, 18, 00, 00));
            var departureTime = "9:30pm";
            var time = TimeFunctions.GetRemainingTime(departureTime);
            var time2 = TimeFunctions.GetRemainingTimeHard(departureTime);
            Console.WriteLine(time);
            Console.WriteLine(time2);
            Console.ReadLine();
        }
    }
}