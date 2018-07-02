using RoslynCore;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using static RoslynCore.NodeFactory;

namespace RoslynTests
{
    [TestClass]
    public class ImmutabilityTests
    {
        [TestMethod]
        public void ReplaceNodeTest()
        {
            var parent = ParentOne(ChildOne());
            parent = parent.WithAnnotation(Annotation("parent"));
            var oldNodes = parent.GetDescendantNodesAndSelf().ToList();
            var child = parent.Identifier.WithAnnotation(Annotation("child"));

            parent = parent.ReplaceNode(parent.Identifier, child);
            var newNodes = parent.GetDescendantNodesAndSelf().ToList();

            Assert.IsTrue(oldNodes.Count == newNodes.Count);
            Assert.IsTrue(oldNodes.Select((n, i) => n != newNodes[i]).All(i => i));
        }

        [TestMethod]
        public void GetRootTest()
        {
            var parent = ParentOne(ChildOne().Cast<ChildOne>().WithType(ChildOne()));
            var node = parent.Identifier.Cast<ChildOne>().Type;

            var root = node.GetRootNode();

            Assert.IsTrue(parent == root);
        }

        [TestMethod]
        public void DescendantNodesTest()
        {
            var parent = ParentOne(ChildOne().Cast<ChildOne>().WithType(ChildOne()));

            var children = parent.GetDescendantNodes().ToList();
            var allNodes = parent.GetDescendantNodesAndSelf().ToList();

            Assert.AreEqual(2, children.Count);
            Assert.AreEqual(3, allNodes.Count);
        }

        [TestMethod]
        public void FindAnnotationTest()
        {
            var childAnntoation = Annotation("child");
            var subChildAnntoation = Annotation("subChild");
            var parent = ParentOne(ChildOne());
            parent = parent.WithAnnotation(Annotation("parent"));
            var oldNodes = parent.GetDescendantNodesAndSelf().ToList();
            var child = parent.Identifier.WithAnnotation(childAnntoation).Cast<ChildOne>().WithType(ChildOne().WithAnnotation(subChildAnntoation));
            parent = parent.ReplaceNode(parent.Identifier, child);
            var newNodes = parent.GetDescendantNodesAndSelf().ToList();

            var findChild = parent.FindDescendantByAnnotation(childAnntoation);
            var findSubChild = parent.FindDescendantByAnnotation(subChildAnntoation);

            Assert.IsNotNull(findChild?.Annotation?.Text);
            Assert.IsNotNull(findSubChild?.Annotation?.Text);
            Assert.AreEqual(childAnntoation.Text, findChild.Annotation.Text);
            Assert.IsNotNull(subChildAnntoation.Text, findSubChild.Annotation.Text);
        }

        [TestMethod]
        public void IndexOfTest()
        {
            var list = new List<int> { 1, 3, 5, 7, 3 }.AsEnumerable();
            var indexOne = list.IndexOf(1);
            var indexTwo = list.IndexOf(3);
            var indexThree = list.IndexOf(9);

            Assert.AreEqual(0, indexOne);
            Assert.AreEqual(1, indexTwo);
            Assert.AreEqual(-1, indexThree);
        }

        [TestMethod]
        public void CloneTest()
        {
            var parentAnnotation = Annotation("parent");
            var childAnnotation = Annotation("child");
            var parent = ParentOne(ChildOne().WithAnnotation(childAnnotation)).WithAnnotation(parentAnnotation);

            var clone = parent.Clone();

            Assert.IsNotNull(clone?.Annotation);
            Assert.IsNotNull(clone.Identifier?.Annotation);
            Assert.AreEqual(parentAnnotation, clone.Annotation);
            Assert.AreEqual(childAnnotation, clone.Identifier.Annotation);
            Assert.AreEqual(parentAnnotation.Text, clone.Annotation.Text);
            Assert.AreEqual(childAnnotation.Text, clone.Identifier.Annotation.Text);
            Assert.AreEqual(parent, parent.Identifier.Parent);
            Assert.AreEqual(clone, clone.Identifier.Parent);
            Assert.AreNotEqual(parent, clone);
            Assert.AreNotEqual(parent.Identifier, clone.Identifier);
            Assert.AreNotEqual(parent.Identifier.Parent, clone);
        }
    }
}