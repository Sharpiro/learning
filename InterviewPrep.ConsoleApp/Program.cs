using InterviewPrep.Core.Compiler;
using InterviewPrep.Core.LogicGates;
using System;
using System.Linq;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            //const string source = @"
            //    contianer Test
            //    {
            //     function Do()
            //     {
            //      return 2;
            //        }
            //    }
            //    var test = Test.New();
            //    test.Do();
            //";
            //var analyzer = new LexicalAnalyzer(source);
            //var tokens = analyzer.Analayze();
            //var parser = new TokenParser(tokens.ToList());
            //parser.Parse();
            //foreach (var token in tokens)
            //{
            //    Console.WriteLine($"<{token.Type}, {token.Value}>");
            //}
            var andGate = new AndGate();
            andGate.Run();
            Console.ReadLine();
        }
    }
}