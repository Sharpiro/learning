using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;

namespace RoslynCore
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class FieldAnalyzer : DiagnosticAnalyzer
    {
        private const string id = "DS01";
        private const string title = "Test";
        private const string messageFormat = "Test";
        private const string description = "Test";
        private const string category = "Usage";

        private static DiagnosticDescriptor rule = new DiagnosticDescriptor(id, title, messageFormat, category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Field);
            //context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.LocalDeclarationStatement);
        }

        private void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            if (!(context.Symbol.Name.StartsWith("_") && context.Symbol.Name.Length > 1)) return;
            var location = context.Symbol.Locations.Single();
            context.ReportDiagnostic(Diagnostic.Create(rule, location));
        }
    }
}