using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using DSharpAnalyzer;

namespace DSharpAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConstructorArgumentAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "DS01";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Initialization";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            //context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.Parameter);
        }

        private void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var parameter = context.Node as ParameterSyntax;
            var parameterList = parameter?.Parent as ParameterListSyntax;
            var constructor = parameterList?.Parent as ConstructorDeclarationSyntax;
            if (constructor == null) return;

            var symbolInfo = context.SemanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
            if (symbolInfo == null || symbolInfo.IsValueType) return;

            var hasNullCheck = constructor.Body.Statements.Any(s => s.DescendantNodes().OfType<ThrowStatementSyntax>()
            .Any(ts => ts.DescendantNodes().OfType<IdentifierNameSyntax>()
            .Any(idn => idn.Identifier.ValueText == parameter.Identifier.ValueText)));

            if (hasNullCheck) return;
            context.ReportDiagnostic(Diagnostic.Create(Rule, parameter.GetLocation(), parameter.GetText()));
        }

        //private static void AnalyzeSymbol(SymbolAnalysisContext context)
        //{
        //    var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;

        //    if (namedTypeSymbol.Name.ToCharArray().Any(char.IsLower))
        //    {
        //        var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);

        //        context.ReportDiagnostic(diagnostic);
        //    }
        //}
    }
}
