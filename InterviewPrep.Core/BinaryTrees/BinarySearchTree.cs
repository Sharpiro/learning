using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.BinaryTrees
{
    public class BinarySearchTree
    {
        public BstNode Root { get; private set; }

        public static BinarySearchTree CreateBalancedBST(IList<int> list)
        {
            var tree = new BinarySearchTree();
            PrivateCreateBalancedBST(list, tree, 0, list.Count - 1);
            return tree;
            throw new NotImplementedException();
        }
        public static void PrivateCreateBalancedBST(IList<int> list, BinarySearchTree tree, int lowIndex, int highIndex)
        {
            if (lowIndex > highIndex)
                return;
            int pivot = (lowIndex + highIndex) / 2;
            highIndex = lowIndex == highIndex ? 0 : highIndex;

            tree.Add(list[pivot]);
            PrivateCreateBalancedBST(list, tree, lowIndex, pivot - 1);
            PrivateCreateBalancedBST(list, tree, pivot + 1, highIndex);
        }

        public void Delete(int data)
        {
            var list = InOrderTraversal();
            list.Remove(data);
            var newList = CreateBalancedBST(list);
            Root = newList.Root;
        }

        public IList<int> PreOrderTraversal()
        {
            var list = new List<int>();
            PrivatePreOrderTraversal(Root, list);
            return list;
        }

        private void PrivatePreOrderTraversal(BstNode currentNode, IList<int> list)
        {
            if (currentNode != null)
            {
                list.Add(currentNode.Data);
                PrivatePreOrderTraversal(currentNode.Left, list);
                PrivatePreOrderTraversal(currentNode.Right, list);
            }
        }

        public IList<int> InOrderTraversal()
        {
            var list = new List<int>();
            PrivateInOrderTraversal(Root, list);
            return list;
        }

        public void PrivateInOrderTraversal(BstNode currentNode, IList<int> list)
        {
            if (currentNode != null)
            {
                PrivateInOrderTraversal(currentNode.Left, list);
                list.Add(currentNode.Data);
                PrivateInOrderTraversal(currentNode.Right, list);
            }
        }

        public IList<int> PostOrderTraversal()
        {
            var list = new List<int>();
            PrivatePostOrderTraversal(Root, list);
            return list;
        }

        public void PrivatePostOrderTraversal(BstNode currentNode, IList<int> list)
        {
            if (currentNode != null)
            {
                PrivatePostOrderTraversal(currentNode.Left, list);
                PrivatePostOrderTraversal(currentNode.Right, list);
                list.Add(currentNode.Data);
            }
        }

        public int GetTreeHeight()
        {
            return PrivateGetTreeHeight(Root);
        }

        private int PrivateGetTreeHeight(BstNode currentNode)
        {
            if (currentNode == null)
                return -1;
            var left = PrivateGetTreeHeight(currentNode.Left);
            var right = PrivateGetTreeHeight(currentNode.Right);
            var max = Math.Max(left, right) + 1;
            return max;
        }

        public int GetMin()
        {
            return PrivateGetMinRecursive(Root);
        }

        private int PrivateGetMinRecursive(BstNode currentNode)
        {
            if (currentNode.Left == null)
                return currentNode.Data;
            var data = PrivateGetMinRecursive(currentNode.Left);
            return data;
        }

        public int GetMax()
        {
            return PrivateGetMaxRecursive(Root);
        }

        public int PrivateGetMaxRecursive(BstNode currentNode)
        {
            if (currentNode.Right == null)
                return currentNode.Data;
            var data = PrivateGetMaxRecursive(currentNode.Right);
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

        public BstNode Find(int data)
        {
            return PrivateFind(Root, data);
        }

        private BstNode PrivateFind(BstNode current, int data)
        {
            if (current == null)
                return null;
            if (current.Data == data)
                return current;
            if (data <= current.Data)
                return PrivateFind(current.Left, data);
            else
                return PrivateFind(current.Right, data);
        }

        public BstNode Add(int data)
        {
            return InsertRecursive(Root, data);
        }

        private BstNode InsertRecursive(BstNode currentNode, int data)
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
    }

    public class BstNode
    {
        public int Data { get; set; }
        public BstNode Left { get; set; }
        public BstNode Right { get; set; }
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