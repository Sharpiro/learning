using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InterviewPrep.Core.Compiler
{
    public class LexicalAnalyzer
    {
        private Tokens _tokens;
        private string _source;
        private List<Token> _foundTokens = new List<Token>();

        public LexicalAnalyzer(string source)
        {
            _tokens = new Tokens();
            _source = source;
        }

        public IEnumerable<Token> Analayze()
        {
            RemoveNewLines();
            while (_source.Length > 0)
            {
                var token = Advance();
                if (token != null)
                    _foundTokens.Add(token);

            }
            return _foundTokens;
        }

        private Token Advance()
        {
            RemoveWhiteSpace();
            var currentString = string.Empty;
            Token token;
            for (var i = 0; i < _source.Length; i++)
            {
                currentString += _source[i];
                if (_tokens.Contains(currentString))
                {
                    token = _tokens.FirstOrDefault(t => t.Value == currentString);
                    _source = _source.Substring(currentString.Length);
                    if (currentString == "\"")
                    {
                        var endIndex = _source.IndexOf(currentString);
                        currentString = _source.Substring(currentString.Length - 1, endIndex);
                        token.Value = currentString;
                        token.Type = TokenType.StringConstant;
                        _source = _source.Substring(endIndex + 1);

                    }
                    return token;
                }
                else
                {
                    token = GetIdentifier();
                    if (token != null)
                    {
                        _source = _source.Substring(token.Value.Length);
                        return token;
                    }
                }
            }
            return null;
        }

        private Token GetIdentifier()
        {
            var match = Regex.Match(_source, @"[_A-Za-z][_A-Za-z0-9]*").ToString();
            var token = new Token { Value = match, Type = TokenType.Identifier };
            return token;
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
