using Microsoft.CodeAnalysis;
using System;
using System.Linq;

namespace DSharpAnalyzer
{
    public static class NodeExtensions
    {
        public static T FindDescendantByAnnotation<T>(this SyntaxNode syntaxNode, SyntaxAnnotation annotation) where T : SyntaxNode
        {
            return (T)syntaxNode.DescendantNodes().SingleOrDefault(n => n.HasAnnotation(annotation));
        }

        public static T FindAncestorByAnnotation<T>(this SyntaxNode syntaxNode, SyntaxAnnotation annotation) where T : SyntaxNode
        {
            return (T)syntaxNode.Ancestors().SingleOrDefault(n => n.HasAnnotation(annotation));
        }
    }

    public static class MiscExtensions
    {
        private static bool? _isVS2017;
        public static bool IsVS2017
        {
            get
            {
                if (_isVS2017 == null)
                {
                    _isVS2017 = AppDomain.CurrentDomain.BaseDirectory.Contains("2017");
                }
                return _isVS2017.Value;
            }
        }
    }
}