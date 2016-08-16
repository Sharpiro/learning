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
            var tree = new BinarySearchTree();
            tree.Add(5);
            tree.Add(8);
            tree.Add(3);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(5, tree.Root.Data);
            Assert.IsNotNull(tree.Root.Right);
            Assert.AreEqual(8, tree.Root.Right.Data);
            Assert.IsNotNull(tree.Root.Left);
            Assert.AreEqual(3, tree.Root.Left.Data);
            Assert.IsNull(tree.Root.Left.Left);
        }

        [TestMethod]
        public void FindTest()
        {
            var tree = new BinarySearchTree();
            tree.Add(5);
            tree.Add(8);
            tree.Add(3);
            var node1 = tree.Find(5);
            var node2 = tree.Find(8);
            var node3 = tree.Find(3);
            var node4 = tree.Find(99);
            Assert.AreEqual(5, node1.Data);
            Assert.AreEqual(8, node2.Data);
            Assert.AreEqual(3, node3.Data);
            Assert.AreEqual(null, node4);
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
                var minNode = tree.FindMin();
                Assert.AreEqual(listMin, minNode.Data);
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
                var maxNode = tree.FindMax();
                Assert.AreEqual(listMin, maxNode.Data);
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
        public void LevelOrderTest()
        {
            var tree = new BinarySearchTree();
            tree.Add(9);
            tree.Add(3);
            tree.Add(6);
            tree.Add(15);
            tree.Add(11);
            var list = tree.LevelOrderTraversal();
            Assert.AreEqual(9, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(15, list[2]);
            Assert.AreEqual(6, list[3]);
            Assert.AreEqual(11, list[4]);
            list = new List<int>();
            tree.LevelOrderTraversal(list, tree.Find(15));
            Assert.AreEqual(15, list[0]);
            Assert.AreEqual(11, list[1]);
        }

        [TestMethod]
        public void CreateBalancedBSTTest()
        {
            var list = new int[] { 3, 6, 9, 11, 15 };
            var tree = BinarySearchTree.CreateBalancedTree(list);
            var newList = tree.PreOrderTraversal();
            Assert.AreEqual(9, newList[0]);
            Assert.AreEqual(3, newList[1]);
            Assert.AreEqual(6, newList[2]);
            Assert.AreEqual(11, newList[3]);
            Assert.AreEqual(15, newList[4]);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var list = new int[] { 3, 6, 9, 11, 15 };
            var tree = BinarySearchTree.CreateBalancedTree(list);
            tree.Delete(6);
            var newList = tree.PreOrderTraversal();
            Assert.AreEqual(9, newList[0]);
            Assert.AreEqual(3, newList[1]);
            Assert.AreEqual(11, newList[2]);
            Assert.AreEqual(15, newList[3]);
            Assert.AreEqual(list.Count() - 1, newList.Count);
            tree.Delete(11);
            newList = tree.PreOrderTraversal();
            Assert.AreEqual(9, newList[0]);
            Assert.AreEqual(3, newList[1]);
            Assert.AreEqual(15, newList[2]);
            Assert.AreEqual(list.Count() - 2, newList.Count);
            tree.Delete(9);
            newList = tree.PreOrderTraversal();
            Assert.AreEqual(15, newList[0]);
            Assert.AreEqual(3, newList[1]);
            Assert.AreEqual(list.Count() - 3, newList.Count);
        }

        [TestMethod]
        public void GetTreeHeightTest()
        {
            var tree = CreateTree(10);
            var treeHeight = tree.GetTreeHeight();
            Assert.AreEqual(9, treeHeight);
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

        [TestMethod]
        public void ConvertToOrderedLinkedListTest()
        {
            var tree = new BinarySearchTree();
            tree.Add(9);
            tree.Add(3);
            tree.Add(6);
            tree.Add(15);
            tree.Add(11);
            var current = tree.ConvertToOrderedLinkedList();
            var second = current.Right;
            var third = second.Right;
            var fourth = third.Right;
            var fifth = fourth.Right;
            Assert.IsNull(current.Left);
            Assert.AreEqual(3, current.Data);
            Assert.AreEqual(3, second.Left.Data);
            Assert.AreEqual(6, second.Data);
            Assert.AreEqual(6, third.Left.Data);
            Assert.AreEqual(9, third.Data);
            Assert.AreEqual(9, fourth.Left.Data);
            Assert.AreEqual(11, fourth.Data);
            Assert.AreEqual(11, fifth.Left.Data);
            Assert.AreEqual(15, fifth.Data);
            Assert.IsNull(fifth.Right);
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