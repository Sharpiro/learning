using InterviewPrep.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        private static ElevatorSystem _elevatorSystem = new ElevatorSystem();

        public static void Main()
        {
            var floors = new List<int> { 5, 4, 9, 2, 7, 11, 22, 5 };
            var task = _elevatorSystem.Run(floors);
            //var task = _elevatorSystem.Run();
            //while (true)
            //{
            //    var floor = Convert.ToInt32(Console.ReadLine());
            //    _elevatorSystem.QueueFloor(floor);
            //}
            Console.ReadLine();
        }
    }
}