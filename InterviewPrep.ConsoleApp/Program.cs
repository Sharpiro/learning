using System;
using InterviewPrep.Generics;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var helper = new GenericEventHelper<double>();
            helper.ThingHappened += ThingHappened;
            helper.SayHello();
            Console.ReadLine();
        }

        private static void ThingHappened(object sender, CustomEventArgs<int> e)
        {
            Console.WriteLine("Thing Happened in program.cs");
            Console.WriteLine($"prop1: {e.Property1}, prop2: {e.Property2}");
        }
    }
}