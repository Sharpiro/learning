using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace RoslynTests
{
    [TestClass]
    public class SemanticsTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string source =
@"class Foo
{ 
    void Bar()
    { 
        int x = 42; 
        // #findme
    } 
}

class ProtectedClass
{
    protected internal void ProtectedMethod()
    {

    }
}";
            var script = CSharpScript.Create(source);
            var compilaton = script.GetCompilation();
            var compilationUnit = compilaton.SyntaxTrees.Single().GetCompilationUnitRoot();
            var semanticModel = compilaton.GetSemanticModel(compilationUnit.SyntaxTree);

            var enclosingSymbol = semanticModel.GetEnclosingSymbol(31);

            var barMethodPosition = source.IndexOf("#findme");
            var protectedMethod = compilationUnit.DescendantNodes().OfType<MethodDeclarationSyntax>().Single(m => m.Identifier.ValueText == "ProtectedMethod");
            var protectedMethodSymbol = semanticModel.GetDeclaredSymbol(protectedMethod);
            var isAccessible = semanticModel.IsAccessible(barMethodPosition, protectedMethodSymbol);
            //var descendants = compilationUnit.DescendantNodesAndTokens().ToList();
        }

        [TestMethod]
        public void TestTest()
        {
            var i = 0;
            var a = 1 + i++;

            Assert.AreEqual(1, a);

            i = 0;
            a = Add(1, i);

            Assert.AreEqual(1, a);
        }

        private int Add(int x, int y)
        {
            return x + y;
        }
    }

    class Test
    {
        private int _x;
    }
}