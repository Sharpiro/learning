using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace DSharpAnalyzer.New.ParameterChecks
{
    public abstract class ParameterListFix
    {
        protected Document Document { get; set; }
        protected SyntaxAnnotation ParameterListAnnotation { get; set; }
        protected SyntaxAnnotation BlockAnnotation { get; set; }
        protected CancellationToken Token { get; set; }
        protected CompilationUnitSyntax CompilationUnit { get; set; }

        protected ParameterListFix(Document document, CompilationUnitSyntax compilationUnit, SyntaxAnnotation parameterListAnnotation,
            SyntaxAnnotation blockAnnotation, CancellationToken cancellationToken)
        {
            Document = document;
            CompilationUnit = compilationUnit;
            ParameterListAnnotation = parameterListAnnotation;
            BlockAnnotation = blockAnnotation;
            Token = cancellationToken;
        }

        protected abstract Task<Document> CreateFix();

        protected void AddSystemUsing()
        {
            var systemUsing = UsingDirective(IdentifierName("System"));
            var hasSystemUsing = CompilationUnit.Usings.Any(u => u.Name.ToString() == systemUsing.Name.ToString());

            if (hasSystemUsing) return;

            CompilationUnit = CompilationUnit.AddUsings(UsingDirective(IdentifierName("System")));
        }

        protected async Task AddField()
        {
            var parameterList = CompilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(ParameterListAnnotation);
            for (var i = parameterList.Parameters.Count - 1; i >= 0; i--)
            {
                Document = Document.WithSyntaxRoot(CompilationUnit);
                var semanticModel = await Document.GetSemanticModelAsync(Token);
                CompilationUnit = (CompilationUnitSyntax)await Document.GetSyntaxRootAsync(Token);

                parameterList = CompilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(ParameterListAnnotation);
                var parameter = parameterList.Parameters[i];

                var parameterSymbol = semanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
                var fieldIdentifierName = $"_{parameter.Identifier.ValueText}";
                var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
                var constructor = block?.Parent as ConstructorDeclarationSyntax;
                var @class = constructor?.Parent as ClassDeclarationSyntax;
                if (@class == null) throw new ArgumentNullException(nameof(@class));

                //check if field already exists
                var fieldExists = @class.Members.OfType<FieldDeclarationSyntax>().Any(f => semanticModel.GetSymbolInfo(f.Declaration.Type).Symbol.Name == parameterSymbol.Name && f.Declaration.Variables
                    .Any(v => v.Identifier.ValueText == fieldIdentifierName));
                if (fieldExists) continue;

                //add field
                var field = FieldDeclaration(VariableDeclaration(parameter.Type)
                    .WithVariables(SingletonSeparatedList(VariableDeclarator($"_{parameter.Identifier}"))))
                    .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)))
                    .WithLeadingTrivia(Whitespace("    "), Whitespace("    "));

                var classMembers = @class.Members.Insert(0, field);
                var newClass = @class.WithMembers(classMembers);

                CompilationUnit = CompilationUnit.ReplaceNode(@class, newClass);
            }
        }

        protected async Task ModifyParameters(Action<ParameterSyntax, INamedTypeSymbol, int> parameterAction)
        {
            var parameterList = CompilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(ParameterListAnnotation);
            for (var i = 0; i < parameterList.Parameters.Count; i++)
            {
                Document = Document.WithSyntaxRoot(CompilationUnit);
                CompilationUnit = (CompilationUnitSyntax)await Document.GetSyntaxRootAsync(Token);
                parameterList = CompilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(ParameterListAnnotation);
                var parameter = parameterList.Parameters[i];
                var semanticModel = await Document.GetSemanticModelAsync(Token);
                var parameterSymbol = semanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
                if (parameterSymbol.IsValueType) continue;

                parameterAction(parameter, parameterSymbol, i);
            }
        }

        protected void AddBinaryThrowExpression(ParameterSyntax parameter, int index)
        {
            if (ThrowExistsForParameter(parameter)) return;
            var binaryThrowStatement = getBinaryThrowStatement();
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
            var blockStatements = block.Statements.ToImmutableList().Insert(index, binaryThrowStatement);
            CompilationUnit = CompilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));

            ExpressionStatementSyntax getBinaryThrowStatement()
            {
                return ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName($"_{parameter.Identifier.ValueText}"),
                    BinaryExpression(SyntaxKind.CoalesceExpression, IdentifierName(parameter.Identifier.ValueText), ThrowExpression(ObjectCreationExpression
                    (IdentifierName("ArgumentNullException")).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument
                    (InvocationExpression(IdentifierName("nameof")).WithArgumentList(ArgumentList(SingletonSeparatedList(
                    Argument(IdentifierName(parameter.Identifier.ValueText)))))))))))));
            }
        }

        protected void AddStringNullCheck(ParameterSyntax parameter, int index)
        {
            if (ThrowExistsForParameter(parameter)) return;
            var expression = createStringInvocationExpression();
            var ifThrowStatement = GetIfThrowStatement(expression, parameter);
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
            var blockStatements = block.Statements.ToImmutableList().Insert(index, ifThrowStatement);
            CompilationUnit = CompilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));

            InvocationExpressionSyntax createStringInvocationExpression()
            {
                return InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    PredefinedType(Token(SyntaxKind.StringKeyword)), IdentifierName("IsNullOrEmpty"))).WithArgumentList(ArgumentList
                    (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))));
            }
        }

        protected void AddReferenceNullCheck(ParameterSyntax parameter, int index)
        {
            if (ThrowExistsForParameter(parameter)) return;
            var expression = createNullCheckExpression();
            var ifThrowStatement = GetIfThrowStatement(expression, parameter);
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
            var blockStatements = block.Statements.ToImmutableList().Insert(index, ifThrowStatement);
            CompilationUnit = CompilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));

            BinaryExpressionSyntax createNullCheckExpression()
            {
                return BinaryExpression(SyntaxKind.EqualsExpression, IdentifierName(parameter.Identifier),
                    LiteralExpression(SyntaxKind.NullLiteralExpression));
            }
        }

        protected void AddFieldAssignment(ParameterSyntax parameter)
        {
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);

            //remove duplicate assignments
            var fieldName = $"_{parameter.Identifier}";
            var assignmentStatements = block.DescendantNodes().OfType<ExpressionStatementSyntax>().Where(s => ((s.Expression as AssignmentExpressionSyntax)?.Left as IdentifierNameSyntax)?.Identifier.ValueText == fieldName);
            CompilationUnit = CompilationUnit.RemoveNodes(assignmentStatements, SyntaxRemoveOptions.KeepNoTrivia);
            block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);

            //add assignment
            var equalStatement = ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName(fieldName), IdentifierName(parameter.Identifier)))
                .WithLeadingTrivia(CarriageReturnLineFeed);

            var parameterCount = CompilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(ParameterListAnnotation).Parameters.Count;
            var blockStatements = block.Statements.ToImmutableList().Add(equalStatement);
            CompilationUnit = CompilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));
        }

        private bool ThrowExistsForParameter(ParameterSyntax parameter)
        {
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
            if (block == null)
                throw new ArgumentNullException(nameof(block), "The block could not be attained through a constructor or method");

            var hasThrowStatement = block.DescendantNodes().OfType<ThrowStatementSyntax>().Any(n => (n.Expression.DescendantNodes()
           .OfType<IdentifierNameSyntax>().Any(idn => idn.Identifier.ValueText == parameter.Identifier.ValueText)));

            var hasThrowExpression = block.DescendantNodes().OfType<ThrowExpressionSyntax>().Any(n => (n.Expression.DescendantNodes()
                 .OfType<IdentifierNameSyntax>().Any(idn => idn.Identifier.ValueText == parameter.Identifier.ValueText)));

            return hasThrowStatement || hasThrowExpression;
        }

        private IfStatementSyntax GetIfThrowStatement(ExpressionSyntax expression, ParameterSyntax parameter)
        {
            return IfStatement(expression,
                ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(
                    InvocationExpression(IdentifierName("nameof")).WithArgumentList(ArgumentList
                    (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))))))))).WithLeadingTrivia())
                    .WithTrailingTrivia(CarriageReturnLineFeed).WithCloseParenToken(Token(TriviaList(), SyntaxKind.CloseParenToken, TriviaList()));
        }
    }
}
