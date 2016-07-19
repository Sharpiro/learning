using System;

namespace InterviewPrep.Core
{
    public class CustomHashTable
    {
        private Set[] _table;
        private const int _maxSize = 128;

        public CustomHashTable()
        {
            _table = new Set[_maxSize];
        }

        public void Add(int key, int value)
        {
            var hash = GetHash(key);
            if (_table[hash] != null)
                throw new ArgumentException("An item with the same key has already been added.");
            _table[hash] = new Set { Key = key, Value = value };
        }

        public int Get(int key)
        {
            var hash = GetHash(key);
            if (hash > _maxSize || _table[hash] == null)
                throw new ArgumentException("The key does not exist in the table");
            var value = _table[hash].Value;
            return value;
        }

        private int GetHash(int key)
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
