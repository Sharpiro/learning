using System;
using InterviewPrep.Generics;
using InterviewPrep.Generics.Entities;
using System.Linq;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var helper = new ReflectionHelper();
            helper.Do();
            Console.ReadLine();
        }
    }
}