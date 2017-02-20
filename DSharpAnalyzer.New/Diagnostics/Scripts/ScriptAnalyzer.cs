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
    public class ScriptAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "DS03";
        private const string Category = "Initialization";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.DS03Title), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.DS03MessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.DS03Description), Resources.ResourceManager, typeof(Resources));
        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Info, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.CompilationUnit);
        }

        private void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            try
            {
                //var compilation = context.
                var compilationUnit = context.Node as CompilationUnitSyntax;

               
                //context.ReportDiagnostic(Diagnostic.Create(Rule, diagnosticLocation, constructor.Identifier));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred in the constructor analyzer", ex);
            }
        }
    }
}