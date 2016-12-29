using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Linq;

namespace RoslynCore
{
    public class Class1
    {
        public void Do()
        {
            const string source = "class Test {}";
            var script = CSharpScript.Create(source);
            var compilaton = script.GetCompilation().SyntaxTrees.Single().GetCompilationUnitRoot();
            var descendants = compilaton.DescendantNodesAndTokens().ToList();
        }
    }
}
