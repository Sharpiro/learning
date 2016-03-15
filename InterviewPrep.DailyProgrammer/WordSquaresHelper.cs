using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewPrep.DailyProgrammer
{
    public class WordSquaresHelper
    {
        private readonly string _dictionaryPath;
        private IEnumerable<string> _dictionary;
        private int _size;
        private string _data;
        private IList<string> _matches;
        private IList<string> _newMatches;

        public WordSquaresHelper(string dictionaryPath)
        {
            _dictionaryPath = dictionaryPath;
            _matches = new List<string>();
            _newMatches = new List<string>();
        }

        public void Find(string input)
        {
            HandleInput(input);
            _dictionary = File.ReadAllLines(_dictionaryPath).Where(d => d.Length == _size).ToList();
            GetWordsInData();
            //CheckSquare();
            //var temp = _matches.Aggregate(string.Empty, (current, match) => $"{current}\n{match}");
            const string word = "rose";
            DoRecursion(word, 0);
            Clear();
        }

        private void DoRecursion(string word, int position)
        {
            foreach (var character in word)
            {
                foreach (var word2 in _matches)
                {
                    if (word2[position] == character)
                    {
                        DoRecursion(word2, position + 1);
                    }
                }
            }
        }

        private void CheckSquare()
        {
            foreach (var word in _matches)
            {
                var counter = 0;
                var addWord = false;
                foreach (var character in word)
                {
                    foreach (var x in _matches)
                    {
                        if (character == x.FirstOrDefault())
                        {
                            counter++;
                            if (counter == _size)
                            {
                                addWord = true;
                            }
                            break;
                        }
                    }
                    if (addWord)
                    {
                        _newMatches.Add(word);
                        addWord = false;
                    }
                }
            }
        }

        private void GetWordsInData()
        {
            foreach (var word in _dictionary)
            {
                var isMatch = CheckWord(word);
                if (isMatch)
                {
                    _matches.Add(word);
                }
            }
        }

        private bool CheckWord(string word)
        {
            var currentData = _data;
            foreach (var character in word)
            {
                if (!currentData.Contains(character))
                {
                    return false;
                }
                var index = currentData.IndexOf(character);
                currentData = currentData.Remove(index, 1);
            }
            return true;
        }

        private void HandleInput(string input)
        {
            var split = input.Split(' ');
            if (split.Length != 2)
                throw new ArgumentException("the input must contain 2 values", nameof(split));
            int.TryParse(split.FirstOrDefault(), out _size);
            if (_size <= 0)
                throw new ArgumentOutOfRangeException(nameof(_size), "other");
            _data = split.LastOrDefault();
            if (_data != null && _data.Length != (int)Math.Pow(_size, 2))
                throw new ArgumentException("the data must be equal to Math.Pow(n, 2)", nameof(_data));
        }

        private void Clear()
        {
            _size = 0;
            _data = null;
        }
    }
}
