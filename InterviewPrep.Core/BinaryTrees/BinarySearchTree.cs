using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.BinaryTrees
{
    public class BinarySearchTree : IBinarySearchTree
    {
        public Node Root { get; set; }

        public Node Add(int data)
        {
            return Add(Root, data);
        }
        public Node Add(Node current, int data)
        {
            if (current == null)
            {
                current = new Node { Data = data };
                if (Root == null)
                    Root = current;
            }
            else if (data <= current.Data)
            {
                current.Left = Add(current.Left, data);
            }
            else
            {
                current.Right = Add(current.Right, data);
            }
            return current;
        }

        public Node Find(int data)
        {
            return Find(Root, data);
        }

        public Node Find(Node current, int data)
        {
            if (current == null)
                return null;
            else if (data == current.Data)
                return current;
            else if (data < current.Data)
                return Find(current.Left, data);
            else
                return Find(current.Right, data);
        }

        public Node FindMin()
        {
            return FindMin(Root);
        }

        public Node FindMin(Node current)
        {
            if (current == null)
                return null;
            if (current.Left == null)
                return current;
            return FindMin(current.Left);
        }

        public Node FindMax()
        {
            return FindMax(Root);
        }

        public Node FindMax(Node current)
        {
            if (current == null)
                return null;
            if (current.Right == null)
                return current;
            return FindMax(current.Right);
        }

        public Node Delete(int data)
        {
            return Delete(Root, data);
        }

        public Node Delete(Node current, int data)
        {
            if (current == null)
                return null;
            else if (data < current.Data)
                current.Left = Delete(current.Left, data);
            else if (data > current.Data)
                current.Right = Delete(current.Right, data);
            else // if equal
            {
                //if tree node
                if (current.Left == null && current.Right == null)
                    current = null;
                //if left subtree is null
                else if (current.Left == null)
                    current = current.Right;
                //if right subtree is null
                else if (current.Right == null)
                    current = current.Left;
                //if node has 2 children
                else
                {
                    var minSubtreeNode = FindMin(current.Right);
                    current.Data = minSubtreeNode.Data;
                    current.Right = Delete(current.Right, minSubtreeNode.Data);
                }
            }
            return current;
        }

        public Node ConvertToOrderedLinkedList()
        {
            var head = FindMin(ConvertToLinkedList(Root));
            return head;
        }

        public Node ConvertToLinkedList()
        {
            var head = ConvertToLinkedList(Root);
            return head;
        }

        public Node ConvertToLinkedList(Node current)
        {
            if (current == null)
                return null;
            current.Left = FindMax(ConvertToLinkedList(current.Left));
            if (current.Left != null)
                current.Left.Right = current;
            current.Right = FindMin(ConvertToLinkedList(current.Right));
            if (current.Right != null)
                current.Right.Left = current;
            return current;
        }

        public List<int> PreOrderTraversal()
        {
            var list = new List<int>();
            PreOrderTraversal(list, Root);
            return list;
        }

        public void PreOrderTraversal(List<int> list, Node current)
        {
            if (current == null)
                return;
            list.Add(current.Data);
            PreOrderTraversal(list, current.Left);
            PreOrderTraversal(list, current.Right);
        }

        public IList<int> LevelOrderTraversal()
        {
            var list = new List<int>();
            LevelOrderTraversal(list, Root);
            return list;
        }

        public void LevelOrderTraversal(IList<int> list, Node current)
        {
            var queue = new Queue<Node>(new[] { current });
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
                list.Add(current.Data);
            }
        }

        public List<int> InOrderTraversal()
        {
            var list = new List<int>();
            InOrderTraversal(list, Root);
            return list;
        }

        public void InOrderTraversal(List<int> list, Node current)
        {
            if (current == null)
                return;
            InOrderTraversal(list, current.Left);
            list.Add(current.Data);
            InOrderTraversal(list, current.Right);
        }

        public List<int> PostOrderTraversal()
        {
            var list = new List<int>();
            PostOrderTraversal(list, Root);
            return list;
        }

        public void PostOrderTraversal(List<int> list, Node current)
        {
            if (current == null)
                return;
            PostOrderTraversal(list, current.Left);
            PostOrderTraversal(list, current.Right);
            list.Add(current.Data);
        }

        public int GetTreeHeight()
        {
            return GetTreeHeight(Root);
        }

        public int GetTreeHeight(Node current)
        {
            if (current == null)
                return -1;
            var leftHeight = GetTreeHeight(current.Left);
            var rightHeight = GetTreeHeight(current.Right);
            var currentHeight = Math.Max(leftHeight, rightHeight) + 1;
            return currentHeight;
        }

        public static BinarySearchTree CreateBalancedTree(int[] list)
        {
            var tree = new BinarySearchTree();
            CreateBalancedTree(list, tree, 0, list.Length - 1);
            return tree;
        }

        public static void CreateBalancedTree(int[] list, BinarySearchTree tree, int lowIndex, int highIndex)
        {
            if (lowIndex > highIndex)
                return;
            var pivot = (lowIndex + highIndex) / 2;
            tree.Add(list[pivot]);
            CreateBalancedTree(list, tree, lowIndex, pivot - 1);
            CreateBalancedTree(list, tree, pivot + 1, highIndex);
        }
    }

    public class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
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