using InterviewPrep.Core.Compiler;
using System;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            const string source = @"
                public class Test
                {
	                public void Do()
	                {
		                Writer.WriteStuff(""Hello World"");
                    }
                }
                var test = new Test();
            ";
            var analyzer = new LexicalAnalyzer(source);
            var tokens = analyzer.Analayze();
            foreach (var token in tokens)
            {
                Console.WriteLine($"<{token.Type}, {token.Value}>");
            }
            Console.ReadLine();
        }
    }
}