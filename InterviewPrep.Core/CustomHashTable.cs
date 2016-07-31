using System;
using System.Linq;

namespace InterviewPrep.Core
{
    public class CustomHashTable
    {
        public int Length { get { return _table.Count(s => s != null); } }
        public int this[int index]
        {
            get { return Get(index); }
            set { AddOrUpdate(index, value); }
        }

        private readonly int _maxSize;
        private Set[] _table;

        public CustomHashTable(int maxSize = 128)
        {
            _maxSize = maxSize;
            _table = new Set[_maxSize];
        }

        public void Add(int key, int value)
        {
            var hash = GetHash(key);
            if (_table[hash] != null)
                throw new ArgumentException("a value with that key already exists");
            _table[hash] = new Set { Key = key, Value = value };
        }

        public void AddOrUpdate(int key, int value)
        {
            var hash = GetHash(key);
            _table[hash] = new Set { Key = key, Value = value };
        }

        public int GetHash(int key)
        {
            if (Length == _maxSize)
                throw new ArgumentException("The table is full");
            var hash = key % _maxSize;
            while (_table[hash] != null && _table[hash].Key != key)
            {
                hash = (hash + 1) % _maxSize;
            }
            return hash;
        }

        public int Get(int key)
        {
            var hash = GetHash(key);
            if (_table[hash] == null)
                throw new ArgumentException("a value with that key does not exist");
            return _table[hash].Value;
        }

        public void Remove(int key)
        {
            var hash = GetHash(key);
            if (_table[hash] == null)
                throw new ArgumentException("a value with that key does not exist");
            _table[hash] = null;
        }
    }

    public class Set
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }
}
