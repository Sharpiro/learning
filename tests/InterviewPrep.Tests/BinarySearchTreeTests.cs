using System;
using System.Collections.Generic;
using System.Linq;
using InterviewPrep.Core.BinaryTrees;
using Xunit;

namespace InterviewPrep.Tests
{
    public class BinarySearchTreeTests
    {
        [Fact]
        public void AddTest()
        {
            var tree = CreateTree(10);
            Assert.NotNull(tree.Root);
        }

        [Fact]
        public void GetMinTest()
        {
            var tree = CreateTree(10);
            var minIterative = tree.GetMinIterative();
            var minRecursive = tree.GetMinRecursive(tree.Root);
            Assert.Equal(minIterative, 0);
            Assert.Equal(minRecursive, 0);
        }

        [Fact]
        public void GetMaxTest()
        {
            var tree = CreateTree(10);
            var maxIterative = tree.GetMaxIterative();
            var maxRecursive = tree.GetMaxRecursive(tree.Root);
            Assert.Equal(maxIterative, 9);
            Assert.Equal(maxRecursive, 9);
        }

        [Fact]
        public void GetTreeHeightTest()
        {
            var tree = CreateTree(10);
            var treeHeight = tree.GetTreeHeight(tree.Root);
            Assert.Equal(treeHeight, 9);
        }

        [Fact]
        public void GetListPreOrderTest()
        {
            var tree = CreateTree(10);
            var list = new List<int?>();
            tree.PreOrderTraversal(tree.Root, list);
        }

        [Fact]
        public void IsBinarySearchTreeTest()
        {
            var list = CreateRandomList(50);
            //var isBinarySearchTree = BinarySearchTree.IsBinarySearchTree(tree, tree.Root, tree.Root.Data);
        }

        [Fact]
        public void FindMinTest()
        {
            for (var i = 0; i < 50; i++)
            {
                var list = CreateRandomList(50);
                list.Sort();
                var listMin = list.FirstOrDefault();
                var tree = CreateTreeFromList(list);
                var min = tree.Min(tree.Root, tree.Root.Data);
                Assert.Equal(listMin, min);
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
