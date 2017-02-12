using DSharpAnalyzer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        [TestMethod]
        public void SpaceTest()
        {
            var assignmentAnnotation = new SyntaxAnnotation();
            var block = Block(ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                IdentifierName("x"), IdentifierName("y")).WithAdditionalAnnotations(assignmentAnnotation)).NormalizeWhitespace());

            var expression = block.FindDescendantByAnnotation<AssignmentExpressionSyntax>(assignmentAnnotation);
            var newExpression = expression.WithLeadingTrivia(TriviaList(CarriageReturnLineFeed,
                CarriageReturnLineFeed, Whitespace("    ")));
            //.WithTrailingTrivia(TriviaList(CarriageReturnLineFeed));


            var token = Identifier("Test");
            //token.trivia

            block = block.ReplaceNode(expression, newExpression);
            var toString = block.ToString();
        }
    }
}