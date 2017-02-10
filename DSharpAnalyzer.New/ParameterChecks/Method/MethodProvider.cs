using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using Microsoft.CodeAnalysis.CodeActions;

namespace DSharpAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MethodProvider)), Shared]
    public class MethodProvider : CodeFixProvider
    {
        private const string title = "Do parameter checks";

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(MethodAnalyzer.DiagnosticId);

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

                var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ParameterListSyntax>().First();

                context.RegisterCodeFix(
                   CodeAction.Create(
                       title: title,
                       createChangedDocument: c => MethodFix.RunMethodParameterFix(context.Document, declaration, c),
                       equivalenceKey: title),
                   diagnostic);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred registerin code fix", ex);
            }
        }
    }
}