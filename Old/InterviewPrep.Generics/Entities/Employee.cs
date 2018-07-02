using System;

namespace InterviewPrep.Generics.Entities
{
    public class Employee
    {
        public string Name { get; set; }

        //public void Speak()
        //{
        //    Console.WriteLine("speaking..");
        //}

        public void Speak<T>()
        {
            Console.WriteLine("speaking..");
            Console.WriteLine(typeof(T).Name);
        }
    }

    public class BetterEmployee
    {

    }
}
