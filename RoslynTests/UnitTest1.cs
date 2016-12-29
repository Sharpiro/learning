using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string source = "class Test {}";
            var script = CSharpScript.Create(source);
            var compilaton = script.GetCompilation().SyntaxTrees.Single().GetCompilationUnitRoot();
            var descendants = compilaton.DescendantNodesAndTokens().ToList();
        }
    }
}
