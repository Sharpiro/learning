using System.Collections.Generic;

namespace InterviewPrep.Core.BinaryTrees
{
    public interface IBinarySearchTree
    {
        Node Root { get; set; }

        Node Add(int data);
        Node Add(Node current, int data);
        Node ConvertToLinkedList();
        Node ConvertToOrderedLinkedList();
        Node ConvertToLinkedList(Node current);
        Node Delete(int data);
        Node Delete(Node current, int data);
        Node Find(int data);
        Node Find(Node current, int data);
        Node FindMax();
        Node FindMax(Node current);
        Node FindMin();
        Node FindMin(Node current);
        int GetTreeHeight();
        int GetTreeHeight(Node current);
        List<int> InOrderTraversal();
        void InOrderTraversal(List<int> list, Node current);
        IList<int> LevelOrderTraversal();
        void LevelOrderTraversal(IList<int> list, Node current);
        List<int> PostOrderTraversal();
        void PostOrderTraversal(List<int> list, Node current);
        List<int> PreOrderTraversal();
        void PreOrderTraversal(List<int> list, Node current);
    }
}