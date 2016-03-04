using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.BinaryTrees
{
    public class BinarySearchTree
    {
        public BstNode Root { get; private set; }

        private void BalanceTree(BstNode node)
        {

        }

        public void Delete(int data)
        {
        }

        public void PreOrderTraversal(BstNode currentNode, IList<int?> list)
        {
            if (currentNode != null)
            {
                list.Add(currentNode.Data);
                PreOrderTraversal(currentNode.Left, list);
                PreOrderTraversal(currentNode.Right, list);
            }
        }

        public void InOrderTraversal(BstNode currentNode, IList<int?> list)
        {
            if (currentNode != null)
            {
                PreOrderTraversal(currentNode.Left, list);
                list.Add(currentNode.Data);
                PreOrderTraversal(currentNode.Right, list);
            }
        }

        public void PostOrderTraversal(BstNode currentNode, IList<int?> list)
        {
            if (currentNode != null)
            {
                PreOrderTraversal(currentNode.Left, list);
                PreOrderTraversal(currentNode.Right, list);
                list.Add(currentNode.Data);
            }
        }

        public int GetTreeHeight(BstNode currentNode)
        {
            if (currentNode == null)
                return -1;
            var left = GetTreeHeight(currentNode.Left);
            var right = GetTreeHeight(currentNode.Right);
            var max = Math.Max(left, right) + 1;
            return max;
        }

        public int GetMinRecursive(BstNode currentNode)
        {
            if (currentNode.Left == null)
                return currentNode.Data;
            var data = GetMinRecursive(currentNode.Left);
            return data;
        }

        public int GetMaxRecursive(BstNode currentNode)
        {
            if (currentNode.Right == null)
                return currentNode.Data;
            var data = GetMaxRecursive(currentNode.Right);
            return data;
        }

        public int GetMinIterative()
        {
            var tempNode = Root;
            while (tempNode.Left != null)
            {
                tempNode = tempNode.Left;
            }
            return tempNode.Data;
        }

        public int GetMaxIterative()
        {
            var tempNode = Root;
            while (tempNode.Right != null)
            {
                tempNode = tempNode.Right;
            }
            return tempNode.Data;
        }

        private BstNode DeleteRecursive(BstNode currentNode, int data)
        {
            if (currentNode.Data == data)
            {

            }
            return currentNode;
        }

        public BstNode InsertRecursive(BstNode currentNode, int data)
        {
            if (currentNode == null)
            {
                currentNode = new BstNode { Data = data };
                if (Root == null)
                    Root = currentNode;
            }
            else if (data <= currentNode.Data)
            {
                currentNode.Left = InsertRecursive(currentNode.Left, data);
            }
            else if (data > currentNode.Data)
            {
                currentNode.Right = InsertRecursive(currentNode.Right, data);
            }
            return currentNode;
        }

        public static bool IsBinarySearchTree(BinarySearchTree tree, BstNode node, int value)
        {
            return false;
        }

        public int Min(BstNode node, int value)
        {
            if (node == null)
            {
                return int.MaxValue;
            }
            var leftMin = Min(node.Left, node.Data);
            var rightMin = Min(node.Right, node.Data);
            var min = Math.Min(leftMin, rightMin);
            var min2 = Math.Min(min, node.Data);
            var min3 = Math.Min(min2, value);
            return min3;
        }

        public int Max()
        {
            return 1;
        }
    }
}

// Binary Tree: a node cannot have more than 2 children
// Strict/Proper Tree: each node has either 2 or 0 children
// complete Tree: all levels are filled except the last and all are as left as possible
// Perfect Tree: All levels are filled
// Balanced Tree: Maximum "diff" for any node is not more than 1
// Max nodes = (2^i)-1 where i is the "count" of levels (start counting at 1 despite the first level is L-0)
// Min Levels = Log2(n + 1) where n is the "count" of nodes
// Max Levels = n - 1 (essentially a linked list)
// Height of an empty binary tree is -1, with just a root node, the height is 0
// Diff = |height - height| : height of left subtree - height of right subtree abs.
// Binary Search Tree: a binary tree in which for each node, left subtree is less or equal, right subtree is more
// Height of Node = # of edges in longest path from the node to a leaf node
// Depth of Node = # of edges in path from root node to the node
// Traversal: Preorer: DLR, Inorder: LDR, Postorder: LRD