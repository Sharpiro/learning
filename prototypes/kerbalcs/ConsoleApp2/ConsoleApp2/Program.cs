using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        private static List<string> _statements = new List<string>();

        private static void Main(string[] args)
        {
            var source =
@"
log(""first"")
log(""second"")
clear()
for (var i = 10; i >= 0; i--){}";
            try
            {
                var script = CSharpScript.Create(source);
                var root = script.GetCompilation().SyntaxTrees.FirstOrDefault().GetCompilationUnitRoot();
                var nodes = root.DescendantNodes().OfType<GlobalStatementSyntax>().ToList();
                ParseGlobalStatements(nodes);
            }
            catch (Exception ex)
            {

            }
            var finalText = string.Join(Environment.NewLine, _statements);
        }

        private static void ParseGlobalStatements(List<GlobalStatementSyntax> globalStatements)
        {
            foreach (var globalStatement in globalStatements)
            {
                switch (globalStatement.Statement.Kind())
                {
                    case SyntaxKind.ForStatement:
                        ParseForStatement(globalStatement.Statement as ForStatementSyntax);
                        break;
                    case SyntaxKind.ExpressionStatement:
                        ParseExpressionStatement(globalStatement.Statement as ExpressionStatementSyntax);
                        break;
                    default:
                        throw new KeyNotFoundException($"couldn't find statement with name '{globalStatement.Kind()}'");
                }
            }
        }

        private static void ParseForStatement(ForStatementSyntax forStatement)
        {
            var localVarName = "countdown";
            var initValue = 10;
            var conditionalExpression = "countdown = 0";
            var statement = $"FROM {{local {localVarName} is {initValue}.}} {conditionalExpression} STEP {{SET {localVarName} to {localVarName} - 1.}} DO";
        }

        private static void ParseExpressionStatement(ExpressionStatementSyntax expressionStatement)
        {
            var identifierNode = expressionStatement.DescendantNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
            var statementName = identifierNode.Identifier.Text;
            switch (statementName)
            {
                case "log":
                    ParseLogStatement(expressionStatement);
                    break;
                case "clear":
                    ParseClearStatement(expressionStatement);
                    break;
                default:
                    throw new KeyNotFoundException($"couldn't find statement with name '{statementName}'");
            }
        }

        private static void ParseLogStatement(ExpressionStatementSyntax statement)
        {
            var arg = statement.DescendantNodes().OfType<LiteralExpressionSyntax>().Single().GetText().ToString();
            var logStatement = $"PRINT {arg}.";
            _statements.Add(logStatement);
        }

        private static void ParseClearStatement(ExpressionStatementSyntax statement)
        {
            //var arg = statement.DescendantNodes().OfType<LiteralExpressionSyntax>().Single().GetText().ToString();
            var logStatement = "CLEARSCREEN.";
            _statements.Add(logStatement);
        }
    }
}