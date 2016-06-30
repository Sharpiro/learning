using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Compiler
{
    public class Tokens : IEnumerable, IEnumerable<Token>
    {
        private IEnumerable<Token> _tokens;

        public Tokens()
        {
            _tokens = new List<Token>
            {
                new Token { Value = "public" , Type = TokenType.Keyword},
                new Token { Value = "class" , Type = TokenType.Keyword},
                new Token { Value = "void" , Type = TokenType.Keyword},
                new Token { Value = "\"" , Type = TokenType.Symbol},
                new Token { Value = "{" , Type = TokenType.Symbol},
                new Token { Value = "}" , Type = TokenType.Symbol},
                new Token { Value = "(" , Type = TokenType.Symbol},
                new Token { Value = ")" , Type = TokenType.Symbol},
                new Token { Value = ";" , Type = TokenType.Symbol},
                new Token { Value = "." , Type = TokenType.Symbol}
            };
        }

        public bool Contains(string value)
        {
            return _tokens.Select(t => t.Value).Contains(value);
        }

        public IEnumerator<Token> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }
    }
}
