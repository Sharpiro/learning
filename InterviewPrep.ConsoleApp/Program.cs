using InterviewPrep.Generics;
using System;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var user = new RepositoryUser();
            user.UseRepositoryPattern();
            Console.ReadLine();
        }
    }
}