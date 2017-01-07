using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace RoslynAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ConstructorParameterProvider)), Shared]
    public class ConstructorParameterProvider : CodeFixProvider
    {
        private const string title = "Initialize field and null checks";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ConstructorArgumentAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ParameterSyntax>().First();

            //context.RegisterCodeFix(
            //    CodeAction.Create(
            //        title: title,
            //        createChangedSolution: c => MakeUppercaseAsync(context.Document, declaration, c),
            //        equivalenceKey: title),
            //    diagnostic);

            context.RegisterCodeFix(
               CodeAction.Create(
                   title: title,
                   createChangedDocument: c => InsertNullCheck(context.Document, declaration, c),
                   equivalenceKey: title),
               diagnostic);
        }

        private async Task<Document> InsertNullCheck(Document document, ParameterSyntax parameter, CancellationToken cancellationToken)
        {
            var parameterList = parameter.Parent;
            var constructor = parameterList?.Parent as ConstructorDeclarationSyntax;

            var block = constructor?.Body;
            if (block == null) throw new ArgumentNullException(nameof(block));

            var fieldName = $"_{parameter.Identifier}";

            var semanticModel = await document.GetSemanticModelAsync();
            var symbolInfo = semanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;

            ExpressionSyntax conditionalExpression;
            if (symbolInfo.Name == "String")
                conditionalExpression = InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    PredefinedType(Token(SyntaxKind.StringKeyword)), IdentifierName("IsNullOrEmpty"))).WithArgumentList(ArgumentList
                    (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))));
            else
                conditionalExpression = BinaryExpression(SyntaxKind.EqualsExpression, IdentifierName(parameter.Identifier),
                LiteralExpression(SyntaxKind.NullLiteralExpression));

            var ifThrowStatement = IfStatement(conditionalExpression,
                ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(
                    InvocationExpression(IdentifierName("nameof")).WithArgumentList(ArgumentList
                    (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))))))))).WithLeadingTrivia())
                    .WithTrailingTrivia(CarriageReturnLineFeed).WithCloseParenToken(Token(TriviaList(), SyntaxKind.CloseParenToken, TriviaList()));

            var equalStatement = ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName(fieldName), IdentifierName(parameter.Identifier)))
                .WithLeadingTrivia(CarriageReturnLineFeed);

            var newBlock = block.AddStatements(ifThrowStatement, equalStatement);

            var oldRoot = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = oldRoot.ReplaceNode(block, newBlock);

            newBlock = newRoot.FindNode(block.FullSpan) as BlockSyntax;

            constructor = newBlock?.Parent as ConstructorDeclarationSyntax;
            var @class = constructor?.Parent as ClassDeclarationSyntax;
            if (@class == null) throw new ArgumentNullException(nameof(@class));


            var field = FieldDeclaration(VariableDeclaration(parameter.Type)
                .WithVariables(SingletonSeparatedList(VariableDeclarator(fieldName))))
                .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)))
                .WithLeadingTrivia(Whitespace("    "), Whitespace("    "));

            var classMembers = @class.Members.Insert(0, field);
            var newClass = @class.WithMembers(classMembers);
            newRoot = newRoot.ReplaceNode(@class, newClass);

            return document.WithSyntaxRoot(newRoot);
        }

        //private async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        //{
        //    // Compute new uppercase name.
        //    var identifierToken = typeDecl.Identifier;
        //    var newName = identifierToken.Text.ToUpperInvariant();

        //    // Get the symbol representing the type to be renamed.
        //    var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
        //    var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

        //    // Produce a new solution that has all references to that type renamed, including the declaration.
        //    var originalSolution = document.Project.Solution;
        //    var optionSet = originalSolution.Workspace.Options;
        //    var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, newName, optionSet, cancellationToken).ConfigureAwait(false);

        //    // Return the new solution with the now-uppercase type name.
        //    return newSolution;
        //}
    }
}