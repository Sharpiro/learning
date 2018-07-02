using System;
using System.Linq;

namespace InterviewPrep.DailyProgrammer
{
    public class LightSwitcher
    {
        public static string SimpleInput => GetInputSimple();
        public static string HardInput => GetInputHard();

        private bool[] _switches;

        public int SwitchLights(string input)
        {
            var lines = input.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(l => l.Trim()).ToList();
            int totalSwitches;
            int.TryParse(lines.FirstOrDefault(), out totalSwitches);
            lines.Remove(lines.FirstOrDefault());
            _switches = new bool[totalSwitches];
            foreach (var line in lines)
            {
                var data = line.Split(' ');
                int min;
                int max;
                int.TryParse(data.FirstOrDefault(), out min);
                int.TryParse(data.LastOrDefault(), out max);
                if (min > max)
                    SwapNumbers(ref min, ref max);
                if (min < 0 || max < 0 || min > max)
                    throw new ArgumentException();
                SwapSwitches(min, max);
            }
            return _switches.ToList().Count(s => s);
        }

        private void SwapSwitches(int min, int max)
        {
            for (var i = min; i <= max; i++)
            {
                _switches[i] = !_switches[i];
            }
        }

        private void SwapNumbers(ref int min, ref int max)
        {
            var temp = min;
            min = max;
            max = temp;
        }

        private static string GetInputSimple()
        {
            //answer is 7
            const string input = @"10
                3 6
                0 4
                7 3
                9 9";
            return input;
        }

        private static string GetInputHard()
        {
            //answer is 423
            const string input = @"1000
                616 293
                344 942
                27 524
                716 291
                860 284
                74 928
                970 594
                832 772
                343 301
                194 882
                948 912
                533 654
                242 792
                408 34
                162 249
                852 693
                526 365
                869 303
                7 992
                200 487
                961 885
                678 828
                441 152
                394 453";
            return input;
        }
    }
}
