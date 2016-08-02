namespace InterviewPrep.Core
{
    public static class FizzBuzzWoof
    {
        public static string Play(int maxValue)
        {
            string fizzBuzzWoof = null;
            for (var i = 1; i <= maxValue; i++)
            {
                var fizz = Fizz(i);
                var buzz = Buzz(i);
                var woof = Woof(i);
                var output = $"{i}: {fizz} {buzz} {woof}".Trim();
                fizzBuzzWoof += $"{output}\n";
                //Console.WriteLine(output);
                //Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            }
            return fizzBuzzWoof;
        }

        public static int GetFullLineNumber(string data)
        {
            var lines = data.Split('\n');
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToLower().Contains("fizz fizz buzz buzz woof woof"))
                {
                    return i + 1;
                }
            }
            return -1;
        }

        private static string Fizz(int index)
        {
            const string fizz = "Fizz";
            const int fizzNumber = 3;
            var isDivisible = index > 0 && index % fizzNumber == 0;
            var fizzString = isDivisible ? fizz : string.Empty;
            var containsFizz = index.ToString().Contains(fizzNumber.ToString());
            fizzString = containsFizz ? fizzString + $" {fizz}" : fizzString;
            return fizzString;
        }

        private static string Buzz(int index)
        {
            const string buzz = "Buzz";
            const int buzzNumber = 5;
            var isDivisible = index > 0 && index % buzzNumber == 0;
            var buzzString = isDivisible ? buzz : string.Empty;
            var containsBuzz = index.ToString().Contains(buzzNumber.ToString());
            buzzString = containsBuzz ? buzzString + $" {buzz}" : buzzString;
            return buzzString;
        }

        private static string Woof(int index)
        {
            const string woof = "Woof";
            const int woofNumber = 7;
            var isDivisible = index > 0 && index % woofNumber == 0;
            var woofString = isDivisible ? woof : string.Empty;
            var containsWoof = index.ToString().Contains(woofNumber.ToString());
            woofString = containsWoof ? woofString + $" {woof}" : woofString;
            return woofString;
        }
    }
}
