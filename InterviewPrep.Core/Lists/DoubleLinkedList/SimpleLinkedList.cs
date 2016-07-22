using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewPrep.Core.Lists.SingleLinkedList
{
    public class SimpleLinkedList : IEnumerable<int>, ISimpleLinkedList
    {
        public int Count { get; set; }
        public Node First { get { return head; } }
        public Node Last { get { return head.previous; } }

        internal Node head;

        public Node AddBefore(Node node, int value)
        {
            var newNode = new Node(value, this);
            InsertNodeBefore(node, newNode);
            if (node == head)
                head = newNode;
            return newNode;
        }

        public Node AddAfter(Node node, int value)
        {
            var newNode = new Node(value, this);
            InsertNodeBefore(node.next, newNode);
            return newNode;
        }

        public Node AddFirst(int value)
        {
            var newNode = new Node(value, this);
            if (head == null)
                InsertNodeEmptyList(newNode);
            else
            {
                InsertNodeBefore(head, newNode);
                head = newNode;
            }
            return newNode;
        }

        public Node AddLast(int value)
        {
            var newNode = new Node(value, this);
            if (head == null)
                InsertNodeEmptyList(newNode);
            else
                InsertNodeBefore(head, newNode);
            return newNode;
        }

        public Node Find(int value)
        {
            var node = head;
            while (node != null)
            {
                if (node.Value == value)
                    return node;
                node = node.next;
            }
            return null;
        }

        public int FindPos(Node node)
        {
            return FindPosPrivate(node, head, 0);
        }

        private int FindPosPrivate(Node node, Node currentNode, int counter)
        {
            if (node == currentNode)
                return counter;
            var temp = FindPosPrivate(node, currentNode.next, ++counter);
            return temp;
        }

        public void Remove(Node node)
        {
            RemoveNode(node);
        }

        public void RemoveFirst()
        {
            if (head == null)
                throw new InvalidOperationException("head cannot be null");
            RemoveNode(head);
        }

        public void RemoveLast()
        {
            if (head == null)
                throw new InvalidOperationException("head cannot be null");
            RemoveNode(head.previous);
        }

        private void InsertNodeEmptyList(Node newNode)
        {
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

        private void RemoveNode(Node node)
        {
            if (node == node.next)
            {
                head = null;
            }
            else
            {
                node.previous.next = node.next;
                node.next.previous = node.previous;
                if (node == head)
                {
                    head = node.next;
                }
            }
            node.Invalidate();
            Count--;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new LinkedListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class Node
    {
        internal Node next;
        internal Node previous;

        public Node(int value, SimpleLinkedList list)
        {
            Value = value;
            List = list;
        }

        public SimpleLinkedList List { get; private set; }
        public int Value { get; set; }
        public Node Next
        {
            get { return (next == null || next == List.head) ? null : next; }
        }
        public Node Previous
        {
            get { return (previous == null || this == List.head) ? null : next; }
        }

        internal void Invalidate()
        {
            next = null;
            previous = null;
            List = null;
        }
    }

    public class LinkedListEnumerator : IEnumerator<int>
    {
        private int _currentValue;
        private Node _currentNode;
        private int _index;
        private readonly SimpleLinkedList _list;

        public LinkedListEnumerator(SimpleLinkedList list)
        {
            _list = list;
            _currentNode = _list.head;
        }

        public object Current
        {
            get
            {
                return _currentValue;
            }
        }

        int IEnumerator<int>.Current
        {
            get
            {
                return _currentValue;
            }
        }

        public bool MoveNext()
        {
            if (_currentNode == null)
            {
                _index = _list.Count + 1;
                return false;
            }
            _currentValue = _currentNode.Value;
            _currentNode = _currentNode.next;
            _index++;
            if (_currentNode == _list.head)
                _currentNode = null;
            return true;

        }

        public void Reset()
        {
            _currentValue = 0;
            _currentNode = _list.head;
            _index = 0;
        }

        public void Dispose() { }
    }
}