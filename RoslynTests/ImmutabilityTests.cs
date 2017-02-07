using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynTests
{
    [TestClass]
    public class ImmutabilityTests
    {
        [TestMethod]
        public void DocumentTest()
        {
            TypeSyntax type = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.StringKeyword));
            var predefinedType = type as PredefinedTypeSyntax;

            var isString1 = predefinedType?.Keyword.Kind() == SyntaxKind.StringKeyword;
            //var typeKind = type.Kind();
            //var x = type.Keyword.Kind();
        }
    }
}