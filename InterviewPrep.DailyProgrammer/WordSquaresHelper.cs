using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewPrep.DailyProgrammer
{
    //var path = $"{Directory.GetCurrentDirectory()}\\files\\wordsquaresdict.txt";
    //var helper = new WordSquaresHelper(path);
    //const string input = "eeeeddoonnnsssrv";
    //helper.Find(input);
    public class WordSquaresHelper
    {
        private IReadOnlyCollection<string> _dictionary;
        private int _size;
        private string _data;
        private IList<string> _matches;
        private List<string> _wordSquare;

        public WordSquaresHelper(string dictionaryPath)
        {
            _dictionary = File.ReadAllLines(dictionaryPath);
            _matches = new List<string>();
            _wordSquare = new List<string>();
        }

        public void Find(string input)
        {
            _data = input;
            _size = (int)Math.Sqrt(_data.Length);
            _dictionary = _dictionary.Where(d => d.Length == _size).ToList();
            _matches = _dictionary.Where(CheckWord).ToList();
            _matches.Remove("odes");
            //_matches.Remove("odor");
            //_matches = new List<string> { "rose", "oven", "send", "ends" };
            //CheckSquare();
            //foreach (var word in _matches)
            //{
            //    if (_wordSquare.Count == 4)
            //        break;
            //}
            const string word = "rose";
            _wordSquare = new List<string> { word };
            DoRecursion(word, 0);
            Clear();
        }

        //word1 = rose
        //  word2[0] =  word1[1];
        //	    word3[1] = word2[2]
        //		    word4[2] = word3[3]
        //  word3[0] = word1[2]
        //	    word4[2] = word3[3]
        //  word4[0] =  word1[3]
        private bool DoRecursion(string word, int position)
        {
            if (position >= _size)
                throw new InvalidDataException("the position must be less than size");
            Console.WriteLine(position);
            var shouldContinue = false;
            foreach (var nextWord in _matches)
            {
                var counter = 0;
                for (var i = 0; i < _wordSquare.Count; i++)
                {
                    if (_wordSquare[i][i + 1] == nextWord[position])
                    {
                        counter++;
                        if (counter == _wordSquare.Count)
                        {
                            if (position + 1 == _size - 1)
                                _wordSquare.Add(nextWord);
                            return true;
                        }
                    }
                    else
                    {
                        shouldContinue = true;
                        break;
                    }
                }
                if (shouldContinue)
                {
                    shouldContinue = false;
                    continue;
                }

                _wordSquare.Add(nextWord);
                var isMatch = DoRecursion(nextWord, position + 1);
                if (isMatch) // && temp + position == _size - 1)
                    break;
                _wordSquare.RemoveAt(position + 1);
            }
            return false;
        }

        //private void CheckSquare()
        //{
        //    foreach (var word in _matches)
        //    {
        //        var counter = 0;
        //        var addWord = false;
        //        foreach (var character in word)
        //        {
        //            foreach (var x in _matches)
        //            {
        //                if (character == x.FirstOrDefault())
        //                {
        //                    counter++;
        //                    if (counter == _size)
        //                    {
        //                        addWord = true;
        //                    }
        //                    break;
        //                }
        //            }
        //            if (addWord)
        //            {
        //                _newMatches.Add(word);
        //                addWord = false;
        //            }
        //        }
        //    }
        //}

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

        private void Clear()
        {
            _size = 0;
            _data = null;
            _matches = new List<string>();
        }
    }
}
