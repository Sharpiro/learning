using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Lists.SingleLinkedList
{
    public class CustomQueue : IEnumerable<int>
    {
        private int _currentSize;
        private int[] _list = new int[0];
        private int _index;

        public int Length { get { return _index; } }

        public void Enqueue(int data)
        {
            TryResize();
            _list[_index] = data;
            _index++;
        }

        public int Dequeue()
        {
            if (_index < 1)
                throw new ArgumentException("cannot dequeue from an empty queue");
            var value = _list[0];
            for (int i = 0; i < _index; i++)
            {
                _list[i] = _list[i + 1];
            }
            _index--;
            return value;
        }

        private void TryResize()
        {
            if (_currentSize != _index)
                return;
            _currentSize = _currentSize == 0 ? 4 : _currentSize * 2;
            var newList = new int[_currentSize];
            Array.Copy(_list, newList, _list.Length);
            _list = newList;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var list = _list.Where((item, index) => index < _index);
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}