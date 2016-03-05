using System;

namespace InterviewPrep.DailyProgrammer
{
    public class CoconutCounter
    {
        public int CountCocounuts(int numberOfSailors)
        {
            const int monkeyNuts = 1;
            int numberOfCoconuts = -1;
            bool breakOuterLoop;

            for (var i = 2; i < int.MaxValue; i++)
            {
                breakOuterLoop = false;
                var currentNuts = i;
                for (var j = 0; j < numberOfSailors; j++)
                {
                    if (currentNuts % numberOfSailors != 1)
                    {
                        breakOuterLoop = true;
                        break;
                    }
                    var sailorNuts = (int)Math.Floor((double)currentNuts / numberOfSailors);
                    currentNuts = currentNuts - sailorNuts - monkeyNuts;
                }
                if (breakOuterLoop)
                    continue;
                var equalLeftOvers = currentNuts % numberOfSailors == 0;
                if (equalLeftOvers)
                {
                    numberOfCoconuts = i;
                    break;
                }
            }
            return numberOfCoconuts;
        }
    }
}
