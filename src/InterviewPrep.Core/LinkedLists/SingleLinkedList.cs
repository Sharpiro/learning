using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.LinkedLists
{
    public class SingleLinkedList<T>
    {
        public int Count { get; set; }
        private Node<T> _head;
        private Node<T> _current;

        public SingleLinkedList()
        {
            _head = null;
        }

        public void Insert(T content)
        {
            Count++;
            var newNode = new Node<T>
            {
                Value = content
            };
            if (_head == null)
                _head = newNode;
            else
                _current.Next = newNode;
            _current = newNode;
        }

        public Node<T> Get(int position)
        {
            var tempNode = _head;
            Node<T> returnNode = null;
            for (var index = 0; tempNode != null; index++)
            {
                if (index == position - 1)
                {
                    returnNode = tempNode;
                    break;
                }
                tempNode = tempNode.Next;
            }
            return returnNode;
        }

        public Node<T> Delete(int position)
        {
            Node<T> returnNode = null;
            if (position == 1)
            {
                returnNode = _head;
                _head = _head.Next;
                Count--;
            }
            else
            {
                var tempNode = _head.Next;
                var lastNode = _head;
                for (var index = 1; tempNode != null; index++)
                {
                    if (index == position - 1)
                    {
                        returnNode = tempNode;
                        lastNode.Next = tempNode.Next;
                        Count--;
                        break;
                    }
                    lastNode = tempNode;
                    tempNode = tempNode.Next;
                }
            }
            return returnNode;
        }

        public List<T> ToList()
        {
            var list = new List<T>();
            var tempNode = _head;
            while (tempNode != null)
            {
                list.Add(tempNode.Value);
                tempNode = tempNode.Next;
            }
            return list;
        }
    }

    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Value { get; set; }
    }
}
