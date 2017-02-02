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
    public class MethodProvider : CodeFixProvider
    {
        private const string title = "Initialize field and null checks";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ConstructorAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            try
            {
                var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

                var diagnostic = context.Diagnostics.First();
                var diagnosticSpan = diagnostic.Location.SourceSpan;

                var declaration = root.FindToken(diagnosticSpan.Start).Parent.DescendantNodes().OfType<ParameterListSyntax>().First();

                context.RegisterCodeFix(
                   CodeAction.Create(
                       title: title,
                       createChangedDocument: c => ConstructorFix.RunConstructorParameterFix(context.Document, declaration, c),
                       equivalenceKey: title),
                   diagnostic);
            }
            catch (Exception)
            {

            }
        }
    }
}