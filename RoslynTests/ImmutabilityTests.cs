using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
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
            var newParameter = Parameter(Identifier(TriviaList(Space), "newParameter", TriviaList()))
                .WithType(PredefinedType(Token(SyntaxKind.ObjectKeyword)));

            //var newCompilation = compilationUnit.WithUsings(SyntaxFactory.SingletonList(SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System"))));
            var newCompilation = compilationUnit.ReplaceNode(parameter, newParameter);
            var x = newCompilation.FindNode(newParameter.FullSpan);

            var newSource = newCompilation.ToString();

            //var findNode = newCompilation.FindNode(parameter.FullSpan);
            //var xxxx = newCompilation.DescendantNodes().Where(n => n.IsEquivalentTo(parameter));

            //Assert.IsNotNull(findNode);
            //Assert.AreEqual(parameter.GetType(), findNode.GetType());
        }
    }
}