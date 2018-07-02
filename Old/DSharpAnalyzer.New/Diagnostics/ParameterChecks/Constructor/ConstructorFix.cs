using DSharpAnalyzer.New.ParameterChecks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Immutable;

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
            await ModifyParameters(ModifyParameter);
            ReOrderStatements();

            return Document.WithSyntaxRoot(CompilationUnit);
        }

        private void ModifyParameter(ParameterSyntax parameterSyntax, INamedTypeSymbol parameterSymbol, int parameterIndex)
        {
            if (MiscExtensions.IsVS2017)
            {
                if (parameterSymbol.Name == "String")
                {
                    AddStringNullCheck(parameterSyntax, parameterIndex);
                    AddFieldAssignment(parameterSyntax);
                }
                else
                    AddBinaryThrowExpression(parameterSyntax, parameterIndex);
            }
            else
            {
                if (parameterSymbol.Name == "String")
                    AddStringNullCheck(parameterSyntax, parameterIndex);
                else
                    AddReferenceNullCheck(parameterSyntax, parameterIndex);
                AddFieldAssignment(parameterSyntax);
            }
        }

        private void ReOrderStatements()
        {
            var block = CompilationUnit.FindDescendantByAnnotation<BlockSyntax>(BlockAnnotation);
            var statements = block.Statements.ToList();

            var throwStatements = statements.OfType<IfStatementSyntax>()
                .Where(ifSt => ifSt.DescendantNodes().Any(dn => dn.Kind() == SyntaxKind.ThrowStatement)).Cast<StatementSyntax>();
            var fieldAssignments = statements.OfType<ExpressionStatementSyntax>().Where(exp => exp.DescendantNodes()
            .Any(dn => dn.Kind() == SyntaxKind.SimpleAssignmentExpression)).Cast<StatementSyntax>();

            var otherStatements = statements.Where(s => !throwStatements.Contains(s) && !fieldAssignments.Contains(s)).ToImmutableList();
            var firstOtherStatement = otherStatements.FirstOrDefault();
            if (firstOtherStatement != null)
                otherStatements = otherStatements.Replace(firstOtherStatement, firstOtherStatement.WithLeadingTrivia(TriviaList(CarriageReturnLineFeed, CarriageReturnLineFeed)));

            var reorderedStatements = throwStatements.Concat(fieldAssignments).Concat(otherStatements);

            CompilationUnit = CompilationUnit.ReplaceNode(block, block.WithStatements(List(reorderedStatements)));
            var cString = CompilationUnit.NormalizeWhitespace().ToString();
        }
    }
}