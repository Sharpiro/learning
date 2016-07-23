﻿using System;
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
            var tree = CreateTree(5);
            Assert.IsNotNull(tree.Root);
            Assert.IsNotNull(tree.Root.Right);
            Assert.IsNull(tree.Root.Left);
        }

        [TestMethod]
        public void CreateBalancedBSTTest()
        {
            var list = new List<int> { 3, 6, 9, 11, 15 };
            var tree = BinarySearchTree.CreateBalancedBST(list);
        }

        [TestMethod]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetTreeHeightTest()
        {
            var tree = CreateTree(10);
            var treeHeight = tree.GetTreeHeight();
            Assert.AreEqual(treeHeight, 9);
        }

        [TestMethod]
        public void GetMinTest()
        {
            for (var i = 0; i < 50; i++)
            {
                var list = CreateRandomList(50);
                var tree = CreateTreeFromList(list);
                list.Sort();
                var listMin = list.FirstOrDefault();
                var minRecursive = tree.GetMinRecursive();
                var minIterative = tree.GetMinIterative();
                Assert.AreEqual(listMin, minRecursive);
                Assert.AreEqual(listMin, minIterative);
            }
        }

        [TestMethod]
        public void GetMaxTest()
        {
            for (var i = 0; i < 50; i++)
            {
                var list = CreateRandomList(50);
                var tree = CreateTreeFromList(list);
                list.Sort();
                var listMin = list.LastOrDefault();
                var maxRecursive = tree.GetMaxRecursive();
                var maxIterative = tree.GetMaxIterative();
                Assert.AreEqual(listMin, maxRecursive);
                Assert.AreEqual(listMin, maxIterative);
            }
        }

        [TestMethod]
        public void PreOrderTest()
        {
            var tree = new BinarySearchTree();
            tree.Add(9);
            tree.Add(3);
            tree.Add(6);
            tree.Add(15);
            tree.Add(11);
            var list = tree.PreOrderTraversal();
            Assert.AreEqual(9, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(6, list[2]);
            Assert.AreEqual(15, list[3]);
            Assert.AreEqual(11, list[4]);
        }

        [TestMethod]
        public void InOrderTest()
        {
            var tree = new BinarySearchTree();
            tree.Add(9);
            tree.Add(3);
            tree.Add(6);
            tree.Add(15);
            tree.Add(11);
            var list = tree.InOrderTraversal();
            Assert.AreEqual(3, list[0]);
            Assert.AreEqual(6, list[1]);
            Assert.AreEqual(9, list[2]);
            Assert.AreEqual(11, list[3]);
            Assert.AreEqual(15, list[4]);
        }

        [TestMethod]
        public void PostOrderTest()
        {
            var tree = new BinarySearchTree();
            tree.Add(9);
            tree.Add(3);
            tree.Add(6);
            tree.Add(15);
            tree.Add(11);
            var list = tree.PostOrderTraversal();
            Assert.AreEqual(6, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(11, list[2]);
            Assert.AreEqual(15, list[3]);
            Assert.AreEqual(9, list[4]);
        }

        private BinarySearchTree CreateTree(int maxSize)
        {
            var tree = new BinarySearchTree();
            for (var i = 0; i < maxSize; i++)
            {
                tree.Add(i);
            }
            return tree;
        }

        private BinarySearchTree CreateTreeReverse(int minSize, int maxSize)
        {
            var tree = new BinarySearchTree();
            for (var i = maxSize - 1; i >= minSize; i--)
            {
                tree.Add(i);
            }
            return tree;
        }

        private BinarySearchTree CreateTreeFromList(List<int> list)
        {
            var tree = new BinarySearchTree();
            foreach (var number in list)
            {
                tree.Add(number);
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