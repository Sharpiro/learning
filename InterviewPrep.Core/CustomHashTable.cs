using System;
using System.Linq;

namespace InterviewPrep.Core
{
    public class CustomHashTable
    {
        public int Length { get { return _table.Where(set => set != null).Count(); } }

        private readonly int _maxSize;
        private Set[] _table;

        public CustomHashTable(int maxSize = 128)
        {
            _maxSize = maxSize;
            _table = new Set[_maxSize];
        }

        public void Add(int key, int value)
        {
            if (Length == _maxSize)
                throw new ArgumentException("Hash table is full.");
            var hash = GetHash(key);
            if (_table[hash] != null)
                throw new ArgumentException("An item with the same key has already been added.");
            _table[hash] = new Set { Key = key, Value = value };
        }

        public int Get(int key)
        {
            var hash = GetHash(key);
            if (_table[hash] == null)
                throw new ArgumentException("The key does not exist in the table");
            var value = _table[hash].Value;
            return value;
        }

        public int GetHash(int key)
        {
            var hash = key % _maxSize;
            //if set value is null return
            //if set value's keys are equal, return
            //otherwise keep computing
            while (_table[hash] != null && _table[hash].Key != key)
                hash = (hash + 1) % _maxSize;
            return hash;
        }
    }

    public class Set
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }
}
