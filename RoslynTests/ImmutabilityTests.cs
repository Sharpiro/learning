using RoslynCore;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace RoslynTests
{
    [TestClass]
    public class ImmutabilityTests
    {
        [TestMethod]
        public void ReplaceNodeTest()
        {
            var parent = NodeFactory.ParentOne(NodeFactory.ChildOne());
            parent = parent.WithAnnotation<ParentOne>(new Annotation("parent"));
            var oldNodes = parent.GetDescendantNodesAndSelf().ToList();
            var child = parent.Identifier.WithAnnotation<ChildOne>(new Annotation("child"));

            parent = parent.ReplaceNode(parent.Identifier, child);
            var newNodes = parent.GetDescendantNodesAndSelf().ToList();

            Assert.IsTrue(oldNodes.Count == newNodes.Count);
            Assert.IsTrue(oldNodes.Select((n, i) => n != newNodes[i]).All(i => i));
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
            var allNodes = parent.GetDescendantNodesAndSelf().ToList();

            Assert.AreEqual(2, children.Count);
            Assert.AreEqual(3, allNodes.Count);
        }

        [TestMethod]
        public void FindAnnotationTest()
        {
            var childAnntoation = new Annotation("child");
            var subChildAnntoation = new Annotation("subChild");
            var parent = NodeFactory.ParentOne(NodeFactory.ChildOne());
            parent = parent.WithAnnotation<ParentOne>(new Annotation("parent"));
            var oldNodes = parent.GetDescendantNodesAndSelf().ToList();
            var child = parent.Identifier.WithAnnotation<ChildOne>(childAnntoation).WithType(NodeFactory.ChildOne().WithAnnotation<ChildOne>(subChildAnntoation));
            parent = parent.ReplaceNode(parent.Identifier, child);
            var newNodes = parent.GetDescendantNodesAndSelf().ToList();

            var findChild = parent.FindDescendantByAnnotation<ChildOne>(childAnntoation);
            var findSubChild = parent.FindDescendantByAnnotation<ChildOne>(subChildAnntoation);

            Assert.IsNotNull(findChild?.Annotation?.Text);
            Assert.IsNotNull(findSubChild?.Annotation?.Text);
            Assert.AreEqual(childAnntoation.Text, findChild.Annotation.Text);
            Assert.IsNotNull(subChildAnntoation.Text, findSubChild.Annotation.Text);
        }
    }
}