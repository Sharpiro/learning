using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using System;

namespace DSharpAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConstructorAnalyzer : DiagnosticAnalyzer
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
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.ConstructorDeclaration);
        }

        private void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var constructor = context.Node as ConstructorDeclarationSyntax;
            var parameterList = constructor.ParameterList;
            var parameters = parameterList.Parameters;

            if (constructor == null || !parameters.Any()) return;

            var hasNullCheck = false;
            foreach (var parameter in parameters)
            {
                var symbolInfo = context.SemanticModel.GetSymbolInfo(parameter.Type).Symbol as INamedTypeSymbol;
                if (symbolInfo == null || symbolInfo.IsValueType) return;

                var statements = constructor.Body.Statements.ToList();
                //ThrowExpressionSyntax
                var x = statements.SelectMany(s => s.DescendantNodesAndSelf()).Select(s => s.GetType().FullName).Distinct();
                var y = string.Join(Environment.NewLine, x);
                var z = typeof(Microsoft.CodeAnalysis.CSharp.Syntax.ThrowStatementSyntax);
                //var x = constructor.Body.Statements.SelectMany(s => s.DescendantNodesAndSelf().OfType<ThrowStatementSyntax>()).ToList();
                //var y = x.SelectMany(s => s.DescendantNodes().OfType<IdentifierNameSyntax>()).ToList();
                //var z = y.Where(n => n.Identifier.ValueText == parameter.Identifier.ValueText).ToList();

                hasNullCheck = constructor.Body.Statements.Any(s => s.DescendantNodesAndSelf().OfType<ThrowStatementSyntax>()
                    .Any(ts => ts.DescendantNodes().OfType<IdentifierNameSyntax>()
                    .Any(idn => idn.Identifier.ValueText == parameter.Identifier.ValueText
                )));

                if (hasNullCheck) return;
            }
            var diagnosticLocation = Location.Create(context.Node.SyntaxTree, TextSpan.FromBounds(constructor.Span.Start, parameterList.FullSpan.End));
            context.ReportDiagnostic(Diagnostic.Create(Rule, diagnosticLocation, constructor.Identifier));
        }
    }
}