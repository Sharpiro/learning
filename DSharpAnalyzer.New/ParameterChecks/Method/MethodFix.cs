using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace DSharpAnalyzer
{
    public class MethodFix
    {
        private Document _document;
        private readonly SyntaxAnnotation _parameterListAnnotation;
        private readonly SyntaxAnnotation _blockAnnotation;
        private CancellationToken _token;
        private CompilationUnitSyntax _compilationUnit;

        private MethodFix(Document document, CompilationUnitSyntax compilationUnit, SyntaxAnnotation parameterListAnnotation,
            SyntaxAnnotation blockAnnotation, CancellationToken cancellationToken)
        {
            _document = document;
            _compilationUnit = compilationUnit;
            _parameterListAnnotation = parameterListAnnotation;
            _blockAnnotation = blockAnnotation;
            _token = cancellationToken;
        }

        public static async Task<Document> RunConstructorParameterFix(Document document, ParameterListSyntax parameterList, CancellationToken cancellationToken)
        {
            var root = (CompilationUnitSyntax)await document.GetSyntaxRootAsync(cancellationToken);
            var parameterListAnnotation = new SyntaxAnnotation("ParameterListTrackerKind");

            root = root.ReplaceNode(parameterList, parameterList.WithAdditionalAnnotations(parameterListAnnotation));
            parameterList = root.FindDescendantByAnnotation<ParameterListSyntax>(parameterListAnnotation);
            var constructor = parameterList.Parent as ConstructorDeclarationSyntax;
            var blockAnnotation = new SyntaxAnnotation("BlockTrackerKind");
            root = root.ReplaceNode(constructor?.Body, constructor?.Body.WithAdditionalAnnotations(blockAnnotation));
            document = document.WithSyntaxRoot(root);

            var instance = new MethodFix(document, root, parameterListAnnotation, blockAnnotation, cancellationToken);
            return await instance.CreateFix();
        }

        private async Task<Document> CreateFix()
        {
            AddSystemUsing();
            await AddField();
            await AddParameterChecks();
            return _document.WithSyntaxRoot(_compilationUnit);
        }

        private void AddSystemUsing()
        {
            var systemUsing = UsingDirective(IdentifierName("System"));
            var hasSystemUsing = _compilationUnit.Usings.Any(u => u.Name.ToString() == systemUsing.Name.ToString());

            if (hasSystemUsing) return;

            _compilationUnit = _compilationUnit.AddUsings(UsingDirective(IdentifierName("System")));
        }

        private async Task AddField()
        {
            try
            {
                var parameterList = _compilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(_parameterListAnnotation);
                for (var i = parameterList.Parameters.Count - 1; i >= 0; i--)
                {
                    _document = _document.WithSyntaxRoot(_compilationUnit);
                    var semanticModel = await _document.GetSemanticModelAsync(_token);
                    _compilationUnit = (CompilationUnitSyntax)await _document.GetSyntaxRootAsync(_token);

                    parameterList = _compilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(_parameterListAnnotation);
                    var parameter = parameterList.Parameters[i];

                    var parameterSymbol = semanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
                    var fieldIdentifierName = $"_{parameter.Identifier.ValueText}";
                    var block = _compilationUnit.FindDescendantByAnnotation<BlockSyntax>(_blockAnnotation);
                    var constructor = block?.Parent as ConstructorDeclarationSyntax;
                    var @class = constructor?.Parent as ClassDeclarationSyntax;
                    if (@class == null) throw new ArgumentNullException(nameof(@class));

                    //check if field already exists
                    var fieldExists = @class.Members.OfType<FieldDeclarationSyntax>().Any(f => semanticModel.GetSymbolInfo(f.Declaration.Type).Symbol.Name == parameterSymbol.Name && f.Declaration.Variables
                        .Any(v => v.Identifier.ValueText == fieldIdentifierName));
                    if (fieldExists) return;

                    //add field
                    var field = FieldDeclaration(VariableDeclaration(parameter.Type)
                        .WithVariables(SingletonSeparatedList(VariableDeclarator($"_{parameter.Identifier}"))))
                        .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)))
                        .WithLeadingTrivia(Whitespace("    "), Whitespace("    "));

                    var classMembers = @class.Members.Insert(0, field);
                    var newClass = @class.WithMembers(classMembers);

                    _compilationUnit = _compilationUnit.ReplaceNode(@class, newClass);
                }
            }
            catch (Exception)
            {

            }
        }

        private async Task AddParameterChecks()
        {
            _document = _document.WithSyntaxRoot(_compilationUnit);
            var semanticModel = await _document.GetSemanticModelAsync(_token);
            _compilationUnit = (CompilationUnitSyntax)await _document.GetSyntaxRootAsync(_token);

            var parameterList = _compilationUnit.FindDescendantByAnnotation<ParameterListSyntax>(_parameterListAnnotation);
            foreach (var parameter in parameterList.Parameters.Reverse())
            {
                var parameterSymbol = semanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
                if (parameterSymbol.IsValueType) continue;

                if (parameterSymbol.Name == "String")
                {
                    AddStringNullCheck(parameter);
                    AddAssignmentStatement(parameter);
                }
                else if (!MiscExtensions.IsVS2017)
                {
                    AddReferenceNullCheck(parameter);
                    AddAssignmentStatement(parameter);
                }
                else
                    AddBinaryThrowExpression(parameter);
            }
        }

        private void AddBinaryThrowExpression(ParameterSyntax parameter)
        {
            if (BinaryThrowExists()) return;
            var binaryThrowStatement = GetBinaryThrowStatement();
            var block = _compilationUnit.FindDescendantByAnnotation<BlockSyntax>(_blockAnnotation);
            var blockStatements = block.Statements.ToImmutableList().Insert(0, binaryThrowStatement);
            _compilationUnit = _compilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));

            bool BinaryThrowExists()
            {
                return _compilationUnit.DescendantNodes().OfType<ThrowExpressionSyntax>().Any(n => (n.Expression.DescendantNodes()
                .OfType<IdentifierNameSyntax>().Any(idn => idn.Identifier.ValueText == parameter.Identifier.ValueText)));
            }

            ExpressionStatementSyntax GetBinaryThrowStatement()
            {
                return ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName($"_{parameter.Identifier.ValueText}"),
                    BinaryExpression(SyntaxKind.CoalesceExpression, IdentifierName(parameter.Identifier.ValueText), ThrowExpression(ObjectCreationExpression
                    (IdentifierName("ArgumentNullException")).WithArgumentList(ArgumentList(SingletonSeparatedList(Argument
                    (InvocationExpression(IdentifierName("nameof")).WithArgumentList(ArgumentList(SingletonSeparatedList(
                    Argument(IdentifierName(parameter.Identifier.ValueText)))))))))))));
            }
        }

        private void AddStringNullCheck(ParameterSyntax parameter)
        {
            var expression = CreateStringInvocationExpression();
            var ifThrowStatement = GetIfThrowStatement(expression, parameter);
            var block = _compilationUnit.FindDescendantByAnnotation<BlockSyntax>(_blockAnnotation);
            var blockStatements = block.Statements.ToImmutableList().Insert(0, ifThrowStatement);
            _compilationUnit = _compilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));

            InvocationExpressionSyntax CreateStringInvocationExpression()
            {
                return InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    PredefinedType(Token(SyntaxKind.StringKeyword)), IdentifierName("IsNullOrEmpty"))).WithArgumentList(ArgumentList
                    (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))));
            }
        }

        private void AddReferenceNullCheck(ParameterSyntax parameter)
        {
            var expression = CreateNullCheckExpression();
            var ifThrowStatement = GetIfThrowStatement(expression, parameter);
            var block = _compilationUnit.FindDescendantByAnnotation<BlockSyntax>(_blockAnnotation);
            var blockStatements = block.Statements.ToImmutableList().Insert(0, ifThrowStatement);
            _compilationUnit = _compilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));

            BinaryExpressionSyntax CreateNullCheckExpression()
            {
                return BinaryExpression(SyntaxKind.EqualsExpression, IdentifierName(parameter.Identifier),
                    LiteralExpression(SyntaxKind.NullLiteralExpression));
            }
        }

        private void AddAssignmentStatement(ParameterSyntax parameter)
        {
            var block = _compilationUnit.FindDescendantByAnnotation<BlockSyntax>(_blockAnnotation);

            //remove duplicate assignments
            var fieldName = $"_{parameter.Identifier}";
            var assignmentStatements = block.DescendantNodes().OfType<ExpressionStatementSyntax>().Where(s => ((s.Expression as AssignmentExpressionSyntax)?.Left as IdentifierNameSyntax)?.Identifier.ValueText == fieldName);
            _compilationUnit = _compilationUnit.RemoveNodes(assignmentStatements, SyntaxRemoveOptions.KeepNoTrivia);
            block = _compilationUnit.FindDescendantByAnnotation<BlockSyntax>(_blockAnnotation);

            //add assignment
            var equalStatement = ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName(fieldName), IdentifierName(parameter.Identifier)))
                .WithLeadingTrivia(CarriageReturnLineFeed);

            var blockStatements = block.Statements.ToImmutableList().Add(equalStatement);
            _compilationUnit = _compilationUnit.ReplaceNode(block, block.WithStatements(List(blockStatements)));
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