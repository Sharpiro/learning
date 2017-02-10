using DSharpAnalyzer.New.ParameterChecks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DSharpAnalyzer
{
    public class ConstructorFix : ParameterListFix
    {
        private ConstructorFix(Document document, CompilationUnitSyntax compilationUnit, SyntaxAnnotation parameterListAnnotation,
            SyntaxAnnotation blockAnnotation, CancellationToken cancellationToken) :
            base(document, compilationUnit, parameterListAnnotation, blockAnnotation, cancellationToken)
        {

        }

        public static async Task<Document> RunConstructorParameterFix(Document document, ParameterListSyntax parameterList, CancellationToken cancellationToken)
        {
            try
            {
                var root = (CompilationUnitSyntax)await document.GetSyntaxRootAsync(cancellationToken);
                var parameterListAnnotation = new SyntaxAnnotation("ParameterListTrackerKind");

                root = root.ReplaceNode(parameterList, parameterList.WithAdditionalAnnotations(parameterListAnnotation));
                parameterList = root.FindDescendantByAnnotation<ParameterListSyntax>(parameterListAnnotation);
                var constructor = parameterList.Parent as ConstructorDeclarationSyntax;
                var blockAnnotation = new SyntaxAnnotation("BlockTrackerKind");
                root = root.ReplaceNode(constructor?.Body, constructor?.Body.WithAdditionalAnnotations(blockAnnotation));
                document = document.WithSyntaxRoot(root);

                var instance = new ConstructorFix(document, root, parameterListAnnotation, blockAnnotation, cancellationToken);
                return await instance.CreateFix();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while performing the constructor code fix", ex);
            }
        }

        protected override async Task<Document> CreateFix()
        {
            AddSystemUsing();
            await AddField();
            await ModifyParameters(ModifyParameters);
            ReOrderStatements();

            return Document.WithSyntaxRoot(CompilationUnit);
        }

        private void ModifyParameters(ParameterSyntax parameterSyntax, INamedTypeSymbol parameterSymbol)
        {
            if (false/*MiscExtensions.IsVS2017*/)
            {
                if (parameterSymbol.Name == "String")
                    AddStringNullCheck(parameterSyntax);
                else
                    AddBinaryThrowExpression(parameterSyntax);
            }
            else
            {
                if (parameterSymbol.Name == "String")
                    AddStringNullCheck(parameterSyntax);
                else
                    AddReferenceNullCheck(parameterSyntax);
                AddFieldAssignment(parameterSyntax);
            }
        }

        private void ReOrderStatements()
        {
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
            var statements = block.Statements.ToList();

            var throwStatements = statements.OfType<IfStatementSyntax>()
                .Where(ifSt => ifSt.DescendantNodes().Any(dn => dn.Kind() == SyntaxKind.ThrowStatement)).Cast<StatementSyntax>();
            var assignmentStatements = statements.OfType<ExpressionStatementSyntax>().Cast<StatementSyntax>();

            if (!throwStatements.Any() || !assignmentStatements.Any())
                throw new ArgumentException("Error occurred when reordering statements");

            var reorderedStatements = throwStatements.Concat(assignmentStatements);
            CompilationUnit = CompilationUnit.ReplaceNode(block, block.WithStatements(SyntaxFactory.List(reorderedStatements)));
        }
    }
}