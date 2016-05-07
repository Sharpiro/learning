using System;
using InterviewPrep.Generics;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var helper = new GenericEventHelper<int>();
            var obj = new { Id = 1, Name = "name1" };
            helper.Register(() => DoOtherStuff(obj.Id));
            Console.ReadLine();
        }

        private static void DoOtherStuff(int id)
        {
            Console.WriteLine($"some event went down in an action, var: {id}");
        }
    }
}