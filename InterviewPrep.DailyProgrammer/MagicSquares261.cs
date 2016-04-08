using System;
using System.Collections.Generic;
using System.IO;

namespace InterviewPrep.DailyProgrammer
{
    public class MagicSquaresHelper261
    {
        private List<int> _numbers;
        private int _root;

        public void Solve(List<int> numbers)
        {
            var root = Math.Sqrt(numbers.Count);
            if ((root % Math.Floor(root)) != 0)
                throw new InvalidDataException("invalid input");
            _numbers = numbers;
            _root = (int)root;

            if (CheckRows() && CheckColumns() && CheckDiagonals())
                Console.WriteLine("TRUE");
            else
                Console.WriteLine("FALSE");
        }

        private bool CheckRows()
        {
            for (var i = 0; i < _root; i++)
            {
                var sum = 0;
                for (var j = 0; j < _root; j++)
                {
                    var temp = i * _root + j;
                    sum += _numbers[temp];
                }
                if (sum != 15)
                    return false;
            }
            return true;
        }

        private bool CheckColumns()
        {
            for (var i = 0; i < _root; i++)
            {
                var sum = 0;
                for (var j = 0; j < _numbers.Count; j += 3)
                {
                    var temp = i + j;
                    sum += _numbers[temp];
                }
                if (sum != 15)
                    return false;
            }
            return true;
        }

        private bool CheckDiagonals()
        {
            var sum = 0;
            for (var i = 0; i < _numbers.Count; i += 4)
            {
                sum += _numbers[i];
            }
            if (sum != 15)
                return false;
            sum = 0;
            for (var i = 2; i < _numbers.Count - 2; i += 2)
            {
                sum += _numbers[i];
            }
            if (sum != 15)
                return false;
            return true;
        }
    }
}
