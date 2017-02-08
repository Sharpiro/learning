using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewPrep.Core.Lists
{
    public class ComplexLinkedList : IComplexLinkedList, IEnumerable<int>
    {
        internal Node head;

        public int Count { get; set; }
        public Node First => head;
        public Node Last => head.previous;

        public Node AddAfter(Node node, int value)
        {
            var newNode = new Node(this) { Data = value };
            PrivateAddBefore(node.next, newNode);
            return newNode;
        }

        public Node AddBefore(Node node, int value)
        {
            var newNode = new Node(this) { Data = value };
            PrivateAddBefore(node, newNode);

            if (node == head)
                head = newNode;
            return newNode;
        }

        public Node AddFirst(int value)
        {
            var newNode = new Node(this) { Data = value };
            if (head == null)
                AddEmpty(newNode);
            else
                PrivateAddBefore(head, newNode);
            head = newNode;

            return newNode;
        }

        public Node AddLast(int value)
        {
            var newNode = new Node(this) { Data = value };
            if (head == null)
                AddEmpty(newNode);
            else
                PrivateAddBefore(head, newNode);
            return newNode;
        }

        public Node Find(int value)
        {
            var current = head;
            do
            {
                if (current.Data == value)
                    return current;
                current = current.next;
            }
            while (current != head);

            return null;
        }

        public int FindPos(Node findNode)
        {
            var currentNode = head;
            var index = 0;
            do
            {
                if (currentNode == findNode)
                    return index;

                currentNode = currentNode.next;
                index++;
            }
            while (currentNode != head);

            return -1;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new LinkedListEnumerator(this);
        }

        public void Remove(Node node)
        {
            node.previous.next = node.next;
            node.next.previous = node.previous;

            if (node == head)
                head = node.next;

            node.next = null;
            node.previous = null;
            Count--;

            if (Count == 0)
                head = null;
        }

        public void RemoveFirst()
        {
            Remove(head);
        }

        public void RemoveLast()
        {
            Remove(head.previous);
        }

        private void PrivateAddBefore(Node node, Node newNode)
        {
            newNode.previous = node.previous;
            newNode.next = node;

            node.previous.next = newNode;
            node.previous = newNode;
            Count++;
        }

        private void AddEmpty(Node newNode)
        {
            if (Count != 0) throw new InvalidOperationException("The list is not empty");

            newNode.next = newNode;
            newNode.previous = newNode;
            head = newNode;
            Count++;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class Node
    {
        private readonly ComplexLinkedList _list;

        internal Node previous;
        internal Node next;

        public Node(ComplexLinkedList list)
        {
            _list = list;
        }

        public Node Previous => previous == null || this == _list.head ? null : previous;
        public Node Next => next == null || next == _list.head ? null : next;
        public int Data { get; set; }
    }

    public class LinkedListEnumerator : IEnumerator<int>
    {
        private ComplexLinkedList _list;
        private Node _currentNode;

        public int Current { get; set; }

        object IEnumerator.Current => throw new NotImplementedException();

        public LinkedListEnumerator(ComplexLinkedList list)
        {
            _list = list;
            _currentNode = _list.head;
        }

        //worth looking into a .next version?
        public bool MoveNext()
        {
            if (_currentNode == null)
                return false;

            Current = _currentNode.Data;
            _currentNode = _currentNode.Next;
            return true;
        }

        public void Reset()
        {
            _currentNode = _list.head;
        }

        public void Dispose() { }
    }
}