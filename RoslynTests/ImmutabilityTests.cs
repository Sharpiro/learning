using RoslynCore;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace RoslynTests
{
    [TestClass]
    public class ImmutabilityTests
    {
        [TestMethod]
        public void ChildOneCloneTest()
        {
            var parent = NodeFactory.ParentOne(NodeFactory.ChildOne());
            parent = parent.WithAnnotation<ParentOne>(new Annotation("parent"));
            var child = parent.Identifier.WithAnnotation<ChildOne>(new Annotation("child"));

            parent = parent.ReplaceNode<ParentOne>(parent.Identifier, child);
            //child.WithAnnotation<Node>(new Annotation("123"));

            //var method = MethodDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)), Identifier("Do")).WithBody(Block());
            //var body = method.Body.WithStatements(List(new StatementSyntax[] { ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName("x"), IdentifierName("2"))) }));
            //var child = NodeFactory.ChildOne();
            //parent = parent.WithIdentifier(child);
        }

        [TestMethod]
        public void GetRootTest()
        {
            var parent = NodeFactory.ParentOne(NodeFactory.ChildOne().Cast<ChildOne>().WithType(NodeFactory.ChildOne()));
            var node = parent.Identifier.Cast<ChildOne>().Type;

            var root = node.GetRootNode();

            Assert.IsTrue(parent == root);
        }

        [TestMethod]
        public void DescendantNodesTest()
        {
            var parent = NodeFactory.ParentOne(NodeFactory.ChildOne().Cast<ChildOne>().WithType(NodeFactory.ChildOne()));

            var children = parent.GetDescendantNodes().ToList();

            Assert.AreEqual(2, children.Count);
        }
    }
}