using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RoslynTests
{
    [TestClass]
    public class ImmutabilityTests
    {
        [TestMethod]
        public void FindNewNodeFromOldNodeTest()
        {
            const string source =
@"public class Test
{
    public Test(string dependency)
    {

    }

    public void Do(string dependency)
    {

    }
}";
            var script = CSharpScript.Create(source);
            var compilation = script.GetCompilation();
            var compilationUnit = compilation.SyntaxTrees.Single().GetCompilationUnitRoot();
            var parameter = compilationUnit.DescendantNodes().OfType<ParameterSyntax>().First();

            var newCompilation = compilationUnit.WithUsings(SyntaxFactory.SingletonList(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System"))));

            //var findNode = newCompilation.FindNode(parameter.FullSpan);
            var xxxx = newCompilation.DescendantNodes().Where(n => n.IsEquivalentTo(parameter));

            //Assert.IsNotNull(findNode);
            //Assert.AreEqual(parameter.GetType(), findNode.GetType());
        }
    }
}