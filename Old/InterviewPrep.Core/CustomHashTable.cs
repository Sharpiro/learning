using System;
using System.Linq;

namespace InterviewPrep.Core
{
    public class CustomHashTable
    {
        private int _maxSize;
        private Set[] _table;

        public int this[int key]
        {
            get { return Get(key); }
            set { AddOrUpdate(key, value); }
        }

        public int Count => _table.Count(s => s != null);

        public CustomHashTable(int maxSize = 128)
        {
            _maxSize = maxSize;
            _table = new Set[_maxSize];
        }

        public void Add(int key, int value)
        {
            var hash = GetHash(key);
            if (_table[hash] != null)
                throw new ArgumentException("key already exists", nameof(key));
            _table[hash] = new Set { Key = key, Value = value };
        }

        public void AddOrUpdate(int key, int value)
        {
            var hash = GetHash(key);
            if (_table[hash] != null)
                _table[hash].Value = value;
            else
                _table[hash] = new Set { Key = key, Value = value };
        }

        public int Get(int key)
        {
            var hash = GetHash(key);
            if (_table[hash] == null)
                throw new ArgumentException("no value exists with that key", nameof(key));
            return _table[hash].Value;
        }

        public void Remove(int key)
        {
            var hash = GetHash(key);
            if (_table[hash] == null)
                throw new ArgumentException("no value exists with that key", nameof(key));
            _table[hash] = null;
        }

        public int GetHash(int key)
        {
            if (Count == _maxSize) throw new InvalidOperationException("the table is full");

            //var hash = key % _maxSize;
            //while (_table[hash] != null && _table[hash].Key != key)
            //{
            //    hash = (hash + 1) % _maxSize;
            //}
            var hash = key;
            for (int i = 0; i < _maxSize; i++)
            {
                hash = (hash + i) % _maxSize;
                if (_table[hash] == null || _table[hash].Key == key)
                    break;
            }
            return hash;
        }
    }

    public class Set
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }
}