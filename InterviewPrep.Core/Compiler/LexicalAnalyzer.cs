using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InterviewPrep.Core.Compiler
{
    public class LexicalAnalyzer
    {
        private readonly List<string> _tokens = new List<string> { "public", "class", "void", "\"", "{", "}", "(", ")", ";", "." };
        private string _source;
        private List<string> _foundTokens = new List<string>();

        public LexicalAnalyzer(string source)
        {
            _source = source;
        }

        public void Analayze()
        {
            RemoveNewLines();
            while (_source.Length > 0)
            {
                var token = Advance();
                if (token != null)
                    _foundTokens.Add(token);

            }
        }

        private string Advance()
        {
            RemoveWhiteSpace();
            var currentString = string.Empty;
            for (var i = 0; i < _source.Length; i++)
            {
                currentString += _source[i];
                if (_tokens.Contains(currentString))
                {
                    _source = _source.Substring(currentString.Length);
                    if (currentString == "\"")
                    {
                        var endIndex = _source.IndexOf(currentString);
                        currentString = _source.Substring(currentString.Length - 1, endIndex);
                        _source = _source.Substring(endIndex + 1);

                    }
                    return currentString;
                }
                else
                {
                    var identifier = GetIdentifier();
                    if (identifier != null)
                    {
                        _source = _source.Substring(identifier.Length);
                        return identifier;
                    }
                }
            }
            return null;
        }

        private string GetIdentifier()
        {
            //var regex = new Regex("[_A-Za-z][_A-Za-z0-9]*");
            var match = Regex.Match(_source, @"[_A-Za-z][_A-Za-z0-9]*").ToString();
            return match;
        }

        private void RemoveNewLines()
        {
            _source = Regex.Replace(_source, @"\s+", " ");
        }

        private void RemoveWhiteSpace()
        {
            var regex = new Regex(@"\s?");
            _source = regex.Replace(_source, "", 1);
        }
    }
}
