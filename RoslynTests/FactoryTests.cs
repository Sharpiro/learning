using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace RoslynTests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void Test()
        {
            var statement = ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName("_param1"),
                BinaryExpression(SyntaxKind.CoalesceExpression, IdentifierName("param1"), ThrowExpression(ObjectCreationExpression
                (IdentifierName("ArgumentNullException")).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument
                (InvocationExpression(IdentifierName("nameof")).WithArgumentList(ArgumentList(SingletonSeparatedList(
                Argument(IdentifierName("param1")))))))))))));
            var expressionSource = statement.ToString();
        }
    }
}