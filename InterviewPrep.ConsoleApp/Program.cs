using InterviewPrep.Core.Compiler;

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
		                System.Console.WriteLine(""Hello World"");
                    }
                }
            ";
            var analyzer = new LexicalAnalyzer(source);
            analyzer.Analayze();
        }
    }
}