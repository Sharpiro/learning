//using System.Collections.Immutable;
//using System.Composition;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CodeFixes;
//using Microsoft.CodeAnalysis.CodeActions;
//using Microsoft.CodeAnalysis.CSharp.Syntax;

//namespace DSharpAnalyzer
//{
//    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ConstructorParameterProvider)), Shared]
//    public class ConstructorParameterProvider : CodeFixProvider
//    {
//        private const string title = "Initialize field and null checks";

//        public sealed override ImmutableArray<string> FixableDiagnosticIds
//        {
//            get { return ImmutableArray.Create(ConstructorArgumentAnalyzer.DiagnosticId); }
//        }

//        public sealed override FixAllProvider GetFixAllProvider()
//        {
//            return WellKnownFixAllProviders.BatchFixer;
//        }

//        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
//        {
//            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

//            var diagnostic = context.Diagnostics.First();
//            var diagnosticSpan = diagnostic.Location.SourceSpan;

//            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<ParameterSyntax>().First();

//            context.RegisterCodeFix(
//               CodeAction.Create(
//                   title: title,
//                   createChangedDocument: c => ConstructorParameterFix.RunConstructorParameterFix(context.Document, declaration, c),
//                   equivalenceKey: title),
//               diagnostic);
//        }
//    }
//}