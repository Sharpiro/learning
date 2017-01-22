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
using Microsoft.CodeAnalysis.Text;

namespace DSharpAnalyzer
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

            context.RegisterCodeFix(
               CodeAction.Create(
                   title: title,
                   createChangedDocument: c => Whatever.ConstructorParameterFix(context.Document, declaration, c),
                   equivalenceKey: title),
               diagnostic);
        }

        //private async Task<Document> ConstructorParameterFix(Document document, ParameterSyntax parameter, CancellationToken cancellationToken)
        //{
        //    var root = (CompilationUnitSyntax)await document.GetSyntaxRootAsync(cancellationToken);

        //    //if (block == null) throw new ArgumentNullException(nameof(block));

        //    //root = AddSystemUsing(root);
        //    //root = AddField(root, parameter);
        //    //root = await AddNullCheck(root, document, parameter);

        //    return document.WithSyntaxRoot(root);
        //}

        //private BlockSyntax FindBlock(ParameterSyntax parameter)
        //{
        //    var parameterList = parameter.Parent;
        //    var constructor = parameterList?.Parent as ConstructorDeclarationSyntax;
        //    return constructor?.Body;
        //}

        //private CompilationUnitSyntax AddSystemUsing(CompilationUnitSyntax compilationUnit)
        //{
        //    var systemUsing = UsingDirective(IdentifierName("System"));
        //    var hasSystemUsing = compilationUnit.Usings.Any(u => u.Name.ToString() == systemUsing.Name.ToString());

        //    if (!hasSystemUsing)
        //        compilationUnit = compilationUnit.AddUsings(UsingDirective(IdentifierName("System")));

        //    return compilationUnit;
        //}

        //private CompilationUnitSyntax AddField(CompilationUnitSyntax compilationUnit, ParameterSyntax parameter)
        //{
        //    //var block = FindBlock(parameter);
        //    BlockSyntax block = null;
        //    var constructor = block?.Parent as ConstructorDeclarationSyntax;
        //    var @class = constructor?.Parent as ClassDeclarationSyntax;
        //    if (@class == null) throw new ArgumentNullException(nameof(@class));

        //    var field = FieldDeclaration(VariableDeclaration(parameter.Type)
        //        .WithVariables(SingletonSeparatedList(VariableDeclarator($"_{parameter.Identifier}"))))
        //        .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)))
        //        .WithLeadingTrivia(Whitespace("    "), Whitespace("    "));

        //    var classMembers = @class.Members.Insert(0, field);
        //    var newClass = @class.WithMembers(classMembers);
        //    return compilationUnit.ReplaceNode(@class, newClass);
        //}

        //private async Task<CompilationUnitSyntax> AddNullCheck(CompilationUnitSyntax compilationUnit, Document document, ParameterSyntax parameter)
        //{
        //    var semanticModel = await document.GetSemanticModelAsync();
        //    var parameterSymbol = semanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
        //    var block = FindBlock(parameter);

        //    ExpressionSyntax conditionalExpression;
        //    if (parameterSymbol.Name == "String")
        //        conditionalExpression = InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
        //            PredefinedType(Token(SyntaxKind.StringKeyword)), IdentifierName("IsNullOrEmpty"))).WithArgumentList(ArgumentList
        //            (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))));
        //    else
        //        conditionalExpression = BinaryExpression(SyntaxKind.EqualsExpression, IdentifierName(parameter.Identifier),
        //        LiteralExpression(SyntaxKind.NullLiteralExpression));

        //    var ifThrowStatement = IfStatement(conditionalExpression,
        //        ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
        //        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(
        //            InvocationExpression(IdentifierName("nameof")).WithArgumentList(ArgumentList
        //            (SingletonSeparatedList(Argument(IdentifierName(parameter.Identifier)))))))))).WithLeadingTrivia())
        //            .WithTrailingTrivia(CarriageReturnLineFeed).WithCloseParenToken(Token(TriviaList(), SyntaxKind.CloseParenToken, TriviaList()));

        //    var equalStatement = ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName($"_{parameter.Identifier}"), IdentifierName(parameter.Identifier)))
        //        .WithLeadingTrivia(CarriageReturnLineFeed);

        //    var newBlock = block.AddStatements(ifThrowStatement, equalStatement);

        //    return compilationUnit.ReplaceNode(block, newBlock);
        //}
    }
}