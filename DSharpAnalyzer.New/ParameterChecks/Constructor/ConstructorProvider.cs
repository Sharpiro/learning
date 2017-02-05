using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace DSharpAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ConstructorProvider)), Shared]
    public class ConstructorProvider : CodeFixProvider
    {
        private const string title = "Initialize field and null checks";

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(ConstructorAnalyzer.DiagnosticId);
        public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            try
            {
                var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

                var diagnostic = context.Diagnostics.First();
                var diagnosticSpan = diagnostic.Location.SourceSpan;

                var findToken = root.FindToken(diagnosticSpan.Start);
                var descendantNodes = findToken.Parent.AncestorsAndSelf().OfType<ParameterListSyntax>();
                var firstDeclaration = descendantNodes.FirstOrDefault();
                if (firstDeclaration == null)
                    throw new ArgumentNullException(nameof(firstDeclaration), "error finding a declaration when registering code fixes");

                context.RegisterCodeFix(
                   CodeAction.Create(
                       title: title,
                       createChangedDocument: c => ConstructorFix.RunConstructorParameterFix(context.Document, firstDeclaration, c),
                       equivalenceKey: title),
                   diagnostic);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred in the constructor provider", ex);
            }
        }
    }
}