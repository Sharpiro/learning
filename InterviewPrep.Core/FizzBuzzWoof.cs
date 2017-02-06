using System;
using System.Linq;

namespace InterviewPrep.Core
{
    public class FizzBuzzWoof
    {
        private readonly string _data;
        private readonly string[] _stringNumbers;

        public FizzBuzzWoof(string data)
        {
            _data = data;
            _stringNumbers = _data.Split(new[] { ", " }, StringSplitOptions.None);

        }

        public string Play()
        {

            var numbers = _stringNumbers.Select(n => Convert.ToInt32(n)).ToArray();
            for (var i = 0; i < numbers.Length; i++)
            {
                Fizz(numbers[i], i);
                Buzz(numbers[i], i);
                Woof(numbers[i], i);
            }

            var resultData = string.Join(", ", numbers);
            return resultData;
        }

        private void Fizz(int number, int index)
        {
            var isFizz = number % 3 == 0;
            if (isFizz)
                _stringNumbers[index] = "Fizz";
        }

        private void Buzz(int number, int index)
        {
            var isFizz = number % 5 == 0;
            if (isFizz)
                _stringNumbers[index] = "Buzz";
        }

        private void Woof(int number, int index)
        {
            var isFizz = number % 7 == 0;
            if (isFizz)
                _stringNumbers[index] = "Woof";
        }
    }
}