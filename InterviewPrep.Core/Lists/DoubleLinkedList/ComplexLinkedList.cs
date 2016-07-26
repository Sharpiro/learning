using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewPrep.Core.Lists.SingleLinkedList
{
    public class ComplexLinkedList : IEnumerable<int>
    {
        public int Count { get; set; }
        public Node First { get { return head; } }
        public Node Last { get { return head.previous; } }

        internal Node head;

        public Node AddFirst(int data)
        {
            var newNode = new Node(data, this);
            if (head == null)
                PrivateAddEmpty(newNode);
            else
                PrivateAddBefore(head, newNode);
            head = newNode;
            return newNode;
        }

        public Node AddLast(int data)
        {
            var newNode = new Node(data, this);
            if (head == null)
                PrivateAddEmpty(newNode);
            else
                PrivateAddBefore(head, newNode);
            return newNode;
        }

        public Node AddAfter(Node node, int data)
        {
            var newNode = new Node(data, this);
            PrivateAddBefore(node.next, newNode);
            return newNode;
        }

        public Node AddBefore(Node node, int data)
        {
            var newNode = new Node(data, this);
            PrivateAddBefore(node, newNode);
            if (node == head)
                head = newNode;
            return newNode;
        }

        public Node Find(int data)
        {
            //return PrivateFind(head, data);
            return FindIterative(head, data);
        }

        private Node PrivateFind(Node current, int data)
        {
            if (current == null)
                return null;
            if (current.Data == data)
                return current;
            return PrivateFind(current.Next, data);
        }

        private Node FindIterative(Node current, int data)
        {
            Validate(current);
            Node node = null;
            do
            {
                if (current.Data == data)
                    return current;
                current = current.next;
            }
            while (current != head);
            return node;
        }

        public int FindPos(Node findNode)
        {
            return PrivateFindPos(head, findNode, 0);
        }

        public int PrivateFindPos(Node currentNode, Node findNode, int position)
        {
            if (currentNode == null)
                return -1;
            if (currentNode == findNode)
                return position;
            return PrivateFindPos(currentNode.Next, findNode, position + 1);
        }

        private void PrivateAddEmpty(Node newNode)
        {
            Validate(newNode);
            newNode.next = newNode;
            newNode.previous = newNode;
            head = newNode;
            Count++;
        }

        private void PrivateAddBefore(Node node, Node newNode)
        {
            Validate(newNode);
            newNode.previous = node.previous;
            newNode.next = node;
            node.previous.next = newNode;
            node.previous = newNode;
            Count++;
        }

        public void RemoveFirst()
        {
            Remove(head);
        }

        public void RemoveLast()
        {
            if (head == null)
                throw new ArgumentNullException(nameof(head));
            Remove(head.previous);
        }

        public void Remove(Node node)
        {
            Validate(node);
            if (node == node.next)
            {
                head = null;
            }
            else
            {
                node.previous.next = node.next;
                node.next.previous = node.previous;
                if (node == head)
                    head = head.next;
            }
            Invalidate(node);
            Count--;
        }

        private void Validate(Node node)
        {
            if (node == null)
                throw new ArgumentException("node cannot be null", nameof(node));
        }

        private void Invalidate(Node node)
        {
            node.next = null;
            node.previous = null;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new CustomEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CustomEnumerator(this);
        }
    }

    public class Node
    {
        internal Node next;
        internal Node previous;
        private ComplexLinkedList _list;

        public Node(int data, ComplexLinkedList list)
        {
            Data = data;
            _list = list;
        }

        public int Data { get; set; }
        public Node Next
        {
            get { return next == null || next == _list.head ? null : next; }
        }
        public Node Previous
        {
            get { return previous == null || this == _list.head ? null : previous; }
        }
    }

    public class CustomEnumerator : IEnumerator<int>
    {
        private int _currentValue;
        private Node _currentNode;
        private ComplexLinkedList _list;

        public CustomEnumerator(ComplexLinkedList list)
        {
            _list = list;
            _currentNode = _list.head;
        }

        public int Current
        {
            get
            {
                return _currentValue;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _currentValue;
            }
        }

        public bool MoveNext()
        {
            if (_currentNode == null)
                return false;
            _currentValue = _currentNode.Data;
            _currentNode = _currentNode.next == _list.head ? null : _currentNode.next;
            return true;
        }

        public void Reset()
        {
            _currentNode = _list.head;
        }

        public void Dispose() { }
    }
}