using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Lists
{
    public class CustomStack : IEnumerable<int>
    {
        private int[] _list = new int[0];
        private int _maxListSize;
        private int _index;

        public int Length { get { return _index; } }

        public void Push(int data)
        {
            TryResize();
            _list[_index] = data;
            _index++;
        }

        public int Pop()
        {
            if (_index < 1)
                throw new ArgumentException("cannot remove from an empty list");
            var value = _list[_index - 1];
            _list[_index - 1] = 0;
            _index--;
            return value;
        }

        private void TryResize()
        {
            if (_index != _maxListSize)
                return;
            _maxListSize = _maxListSize == 0 ? 4 : _maxListSize * 2;
            var newList = new int[_maxListSize];
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
