using System;
using System.Linq;
using System.Text;

namespace InterviewPrep.Core.LogicGates
{
    public class AndGate
    {
        public void Run()
        {
            var one = "G";
            var two = "E";
            var three = "T";
            var four = "GET";
            var byte1 = Encoding.ASCII.GetBytes(one).FirstOrDefault();
            var byte2 = Encoding.ASCII.GetBytes(two).FirstOrDefault();
            var byte3 = Encoding.ASCII.GetBytes(three).FirstOrDefault();
            var byte4 = Encoding.ASCII.GetBytes(four);
            var binary = GetBinary(byte1);
            var temp = 0 & 1;
            var temp2 = 0 | 1;
            var temp3 = 0 | 0;
            var temp4 = 4 & 5;
            var temp5 = 16 >> 1;
            var temp6 = 64 >> 1;
            var temp7 = 32 << 1;
        }

        private string GetBinary(byte number)
        {
            var currentValue = number;
            var maxIndex = 8;
            StringBuilder binary = new StringBuilder();
            for (var i = maxIndex; i > 0; i--)
            {
                var currentIndex = Convert.ToByte(Math.Pow(2, i - 1));
                if (currentValue >= currentIndex)
                {
                    currentValue -= currentIndex;
                    binary.Append(1);
                }
                else
                    binary.Append(0);
            }
            return binary.ToString();
        }
    }
}
