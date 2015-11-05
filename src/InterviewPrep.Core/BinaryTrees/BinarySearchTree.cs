namespace InterviewPrep.Core.BinaryTrees
{
    public class BinarySearchTree
    {
        private BstNode _root;

        private void BalanceTree(BstNode node)
        {

        }

        public void Insert(int data)
        {
            _root = InsertRecursive(_root, data);
        }

        public void Delete(int data)
        {
            _root = InsertRecursive(_root, data);
        }

        private BstNode DeleteRecursive(BstNode currentNode, int data)
        {
            if (currentNode.Data == data)
            {

            }
            return currentNode;
        }

        private BstNode InsertRecursive(BstNode currentNode, int data)
        {
            if (currentNode == null)
            {
                currentNode = new BstNode { Data = data };
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