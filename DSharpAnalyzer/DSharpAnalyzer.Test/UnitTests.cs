using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using DSharpAnalyzer;

namespace RoslynAnalyzer.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {
        [TestMethod]
        public void AnalyzerTest()
        {
//            var test =
//@"using System;

//namespace RoslynTests
//{
//    public class DummyClass
//    {
//        private readonly object _dep1;

//        public DummyClass(object dep2, object dep1)
//        {
//            _dep1 = dep1 ?? throw new ArgumentNullException(nameof(dep1));
//        }
//    }
//}";

            var test =
@"using System;

namespace RoslynTests
{
    public class DummyClass
    {
        public DummyClass(object dep2, object dep1)
        {
        }
    }
}";
            var expected = new DiagnosticResult
            {
                Id = "DS01",
                Message = String.Format("Type name '{0}' contains lowercase letters", "TypeName"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 11, 15)
                        }
            };

            VerifyCSharpDiagnostic(test);
        }
        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public void CodeFixTest()
        {
            var test = @"
namespace RoslynTests
{
    public class DummyClass
    {
        private readonly string _x;
        private readonly string _y;

        public DummyClass(object x, object y)
        {

        }
    }
}";
            var fixtest =
@"using System;

namespace RoslynTests
{
    public class DummyClass
    {
        private readonly string _x;
        private readonly string _y;

        public DummyClass(string x, string y)
        {
            if (string.IsNullOrEmpty(x))
                throw new ArgumentNullException(nameof(x));
            if (string.IsNullOrEmpty(y))
                throw new ArgumentNullException(nameof(y));

            _x = x;
            _y = y;
        }
    }
}";
            VerifyCSharpFix(test, fixtest);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new ConstructorProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new ConstructorAnalyzer();
        }
    }
}