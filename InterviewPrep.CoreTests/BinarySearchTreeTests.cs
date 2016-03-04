using System;
using System.Collections.Generic;
using System.Linq;
using InterviewPrep.Core.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void AddTest()
        {
            var tree = CreateTree(10);
            Assert.IsNotNull(tree.Root);
        }

        [TestMethod]
        public void GetMinTest()
        {
            var tree = CreateTree(10);
            var minIterative = tree.GetMinIterative();
            var minRecursive = tree.GetMinRecursive(tree.Root);
            Assert.AreEqual(minIterative, 0);
            Assert.AreEqual(minRecursive, 0);
        }

        [TestMethod]
        public void GetMaxTest()
        {
            var tree = CreateTree(10);
            var maxIterative = tree.GetMaxIterative();
            var maxRecursive = tree.GetMaxRecursive(tree.Root);
            Assert.AreEqual(maxIterative, 9);
            Assert.AreEqual(maxRecursive, 9);
        }

        [TestMethod]
        public void GetTreeHeightTest()
        {
            var tree = CreateTree(10);
            var treeHeight = tree.GetTreeHeight(tree.Root);
            Assert.AreEqual(treeHeight, 9);
        }

        [TestMethod]
        public void GetListPreOrderTest()
        {
            var tree = CreateTree(10);
            var list = new List<int?>();
            tree.PreOrderTraversal(tree.Root, list);
        }

        [TestMethod]
        public void IsBinarySearchTreeTest()
        {
            var list = CreateRandomList(50);
            //var isBinarySearchTree = BinarySearchTree.IsBinarySearchTree(tree, tree.Root, tree.Root.Data);
        }

        [TestMethod]
        public void FindMinTest()
        {
            for (var i = 0; i < 50; i++)
            {
                var list = CreateRandomList(50);
                list.Sort();
                var listMin = list.FirstOrDefault();
                var tree = CreateTreeFromList(list);
                var min = tree.Min(tree.Root, tree.Root.Data);
                Assert.AreEqual(listMin, min);
            }
        }

        private BinarySearchTree CreateTree(int maxSize)
        {
            var tree = new BinarySearchTree();
            for (var i = 0; i < maxSize; i++)
            {
                tree.InsertRecursive(tree.Root, i);
            }
            return tree;
        }

        private BinarySearchTree CreateTreeReverse(int minSize, int maxSize)
        {
            var tree = new BinarySearchTree();
            for (var i = maxSize - 1; i >= minSize; i--)
            {
                tree.InsertRecursive(tree.Root, i);
            }
            return tree;
        }

        private BinarySearchTree CreateTreeFromList(List<int> list)
        {
            var tree = new BinarySearchTree();
            foreach (var number in list)
            {
                tree.InsertRecursive(tree.Root, number);
            }
            return tree;
        }

        private List<int> CreateRandomList(int maxSize)
        {
            var list = new List<int>();
            var rand = new Random();
            for (var i = 0; i < maxSize; i++)
            {
                list.Add(rand.Next(maxSize));
            }
            return list;
        }
    }
}
