using System;
using System.Diagnostics;

namespace InterviewPrep.Core.Lists.SingleLinkedList
{
    public class SimpleLinkedList
    {
        public int Count { get; set; }
        public Node First { get { return head; } }
        public Node Last { get { return head.previous; } }

        internal Node head;

        public SimpleLinkedList()
        {

        }

        public void AddBefore()
        {
            throw new NotImplementedException();
        }

        public void AddFirst(int value)
        {
            var newNode = new Node(value, this);
            if (head == null)
                InsertNodeEmptyList(newNode);
            else
            {
                InsertNodeBefore(head, newNode);
                head = newNode;
            }
        }

        public void AddLast(int value)
        {
            var newNode = new Node(value, this);
            if (head == null)
                InsertNodeEmptyList(newNode);
            else
                InsertNodeBefore(head, newNode);
        }

        private void InsertNodeEmptyList(Node newNode)
        {
            Debug.Assert(head == null && Count == 0, "LinkedList must be empty when this method is called!");
            newNode.next = newNode;
            newNode.previous = newNode;
            head = newNode;
            Count++;
        }

        private void InsertNodeBefore(Node node, Node newNode)
        {
            newNode.next = node;
            newNode.previous = node.previous;
            node.previous.next = newNode;
            node.previous = newNode;
            Count++;
        }
    }

    public class Node
    {
        internal Node next;
        internal Node previous;

        private SimpleLinkedList _list;

        public Node(int value, SimpleLinkedList list)
        {
            Value = value;
            _list = list;
        }

        public int Value { get; set; }
        public Node Next
        {
            get { return (next == null || next == _list.head) ? null : next; }
        }
        public Node Previous
        {
            get { return (previous == null || this == _list.head) ? null : next; }
        }
    }
}
