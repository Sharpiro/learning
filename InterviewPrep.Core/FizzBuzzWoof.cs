using System.Text;

namespace InterviewPrep.Core
{
    public class FizzBuzzWoof
    {
        private readonly int _maxNumber;
        private readonly StringBuilder _builder = new StringBuilder();

        public FizzBuzzWoof(int maxNumber)
        {
            _maxNumber = maxNumber;
        }

        public string Play()
        {
            for (var i = 1; i <= _maxNumber; i++)
            {
                var number = i;
                var isFizz = number % 3 == 0;
                var isBuzz = number % 5 == 0;
                var isWoof = number % 7 == 0;

                if (isFizz)
                    _builder.Append($"Fizz({number}), ");

                if (isBuzz)
                    _builder.Append($"Buzz({number}), ");

                if (isWoof)
                    _builder.Append($"Woof({number}), ");

                if (!isFizz && !isBuzz && !isWoof)
                    _builder.Append($"{number}, ");
            }
            _builder.Remove(_builder.Length - 2, 2);
            return _builder.ToString();
        }
    }
}