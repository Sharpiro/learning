using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.Compiler
{
    public class TokenParser
    {
        private readonly IList<Token> _tokens;
        private int _currentIndex;
        private Dictionary<string, string> _variables;

        public TokenParser(IList<Token> tokens)
        {
            _tokens = tokens;
            _variables = new Dictionary<string, string>();
        }

        public void Parse()
        {
            ParseTerm();
        }

        private void ParseTerm()
        {

        }

        private Token PeekToken()
        {
            try
            {
                return _tokens[_currentIndex + 1];
            }
            catch (ArgumentOutOfRangeException) { }
            return null;
        }

        private Token NextToken()
        {
            try
            {
                _currentIndex++;
                return _tokens[_currentIndex];
            }
            catch (ArgumentOutOfRangeException) { }
            return null;
        }
    }
}