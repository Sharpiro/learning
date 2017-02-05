//using DSharpAnalyzer.New.ParameterChecks;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace DSharpAnalyzer
//{
//    public class MethodFix : ParameterListFix
//    {
//        private MethodFix(Document document, CompilationUnitSyntax compilationUnit, SyntaxAnnotation parameterListAnnotation,
//            SyntaxAnnotation blockAnnotation, CancellationToken cancellationToken) :
//            base(document, compilationUnit, parameterListAnnotation, blockAnnotation, cancellationToken)
//        {
//        }

//        public static async Task<Document> RunMethodParameterFix(Document document, ParameterListSyntax parameterList, CancellationToken cancellationToken)
//        {
//            var root = (CompilationUnitSyntax)await document.GetSyntaxRootAsync(cancellationToken);
//            var parameterListAnnotation = new SyntaxAnnotation("ParameterListTrackerKind");

//            root = root.ReplaceNode(parameterList, parameterList.WithAdditionalAnnotations(parameterListAnnotation));
//            parameterList = root.FindDescendantByAnnotation<ParameterListSyntax>(parameterListAnnotation);
//            var method = parameterList.Parent as MethodDeclarationSyntax;
//            var blockAnnotation = new SyntaxAnnotation("BlockTrackerKind");
//            root = root.ReplaceNode(method.Body, method?.Body.WithAdditionalAnnotations(blockAnnotation));
//            document = document.WithSyntaxRoot(root);

//            var instance = new MethodFix(document, root, parameterListAnnotation, blockAnnotation, cancellationToken);
//            return await instance.CreateFix();
//        }

//        protected override async Task<Document> CreateFix()
//        {
//            try
//            {
//                AddSystemUsing();
//                await AddParameterChecks(CheckParameter);
//                return Document.WithSyntaxRoot(CompilationUnit);
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        private void CheckParameter(INamedTypeSymbol parameterSymbol, ParameterSyntax parameter)
//        {
//            if (parameterSymbol.Name == "String")
//                AddStringNullCheck(parameter);
//            else
//                AddReferenceNullCheck(parameter);
//        }
//    }
//}