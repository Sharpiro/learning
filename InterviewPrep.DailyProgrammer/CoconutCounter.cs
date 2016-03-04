using System;
using System.IO;

namespace InterviewPrep.DailyProgrammer
{
    public class CoconutCounter
    {
        public int CountCocounuts(int numberOfSailors)
        {
            var startingNuts = 3121;
            var currentNuts = startingNuts;
            //#OfSailors = 5;
            //1 % n = 1;
            for (var i = 0; i < 1; i++)
            {
                if (currentNuts % numberOfSailors != 1)
                    throw new InvalidDataException();
                var sailorNuts = (int)Math.Floor((double)currentNuts / numberOfSailors);
                var monkeyNuts = 1;
                SimpleMath1(currentNuts, sailorNuts, monkeyNuts);
                SimpleMath2(currentNuts, sailorNuts, monkeyNuts);
                SimpleMath3(currentNuts, sailorNuts, monkeyNuts);

            }
            return -1;
        }

        private void SimpleMath1(int currentNuts, int sailorNuts, int monkeyNuts)
        {
            currentNuts = currentNuts - sailorNuts - monkeyNuts;
            Console.WriteLine(currentNuts);
        }

        private void SimpleMath2(int currentNuts, int sailorNuts, int monkeyNuts)
        {
            currentNuts -= sailorNuts;
            currentNuts -= monkeyNuts;
            Console.WriteLine(currentNuts);
        }

        private void SimpleMath3(int currentNuts, int sailorNuts, int monkeyNuts)
        {
            // x = 5 + -3 + 1;
            //x = 5 - 3;
            //x = x - 1;
            //x = 3121 + -624 + -1
            currentNuts -= sailorNuts + monkeyNuts;
            Console.WriteLine(currentNuts);
        }
    }
}
