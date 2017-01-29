using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RoslynCore
{
    /// <summary>
    /// requires the following dlls in executing project as well
    /// Microsoft.CodeAnalysis.CSharp
    /// Microsoft.CodeAnalysis.CSharp.Workspaces
    /// </summary>
    public class Workspace
    {
        public MSBuildWorkspace MSBuildWorkspace { get; private set; }
        public Solution Solution { get; private set; }
        public Project Project { get; private set; }
        public Document Document { get; private set; }

        private Workspace() { }

        public static Workspace Create()
        {
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(@"C:\Users\sharpiro\Documents\Visual Studio 2015\Projects\DummyCode\DummyCode.sln").Result;
            var project = solution.Projects.Single(p => p.Name == "DummyCode");
            var document = project.Documents.Single(d => d.Name == "Dummy.cs");
            return new Workspace { MSBuildWorkspace = workspace, Solution = solution, Project = project, Document = document };
        }

        public void GetTree()
        {
            var graph = Solution.GetProjectDependencyGraph();
            var projects = graph.GetTopologicallySortedProjects();
            //projects
            foreach (var pId in projects)
            {
                var project = Solution.GetProject(pId);
                //references
                foreach (var reference in project.ProjectReferences)
                {

                }
                //documents
                foreach (var document in project.Documents)
                {

                }
            }
        }

        public void Classify()
        {
            var syntaxTree = Document.GetSyntaxTreeAsync().Result;
            var syntaxTreeRoot = syntaxTree.GetRoot();
            var spans = Classifier.GetClassifiedSpansAsync(Document, syntaxTreeRoot.FullSpan).Result;
            var spanDictionary = spans.ToDictionary(s => s.TextSpan.Start, s => s);
            var data = new List<Tuple<char, ConsoleColor>>();

            var text = syntaxTree.ToString();

            var i = 0;
            var color = ConsoleColor.Gray;
            foreach (var character in text)
            {
                ClassifiedSpan span;
                if (spanDictionary.TryGetValue(i, out span))
                {
                    switch (span.ClassificationType)
                    {
                        case ClassificationTypeNames.ClassName:
                            color = ConsoleColor.Blue;
                            break;
                        default:
                            color = ConsoleColor.Gray;
                            break;
                    }
                    //Console.ForegroundColor = color;
                }
                data.Add(Tuple.Create(character, color));
                //Console.Write(character);
                i++;
            }
        }

        public void Format()
        {
            var newDocument = Formatter.FormatAsync(Document).Result;
            var source = newDocument.GetSyntaxRootAsync().Result.ToString();
        }

        public void FindSymbols()
        {
            var projectPath = Path.GetFullPath("../../../RoslynCore/RoslynCore.csproj");
            var workspace = MSBuildWorkspace.Create();
            var project = workspace.OpenProjectAsync(projectPath).Result;

            var compilation = project.GetCompilationAsync().Result;
            var type = compilation.GetTypeByMetadataName("RoslynCore.ReferencesClass");
            var method = type.GetMembers().Single(m => m.Name == "DoStuff");
            var property = type.GetMembers().Single(m => m.Name == "Property");

            //find "callers" for the "DoStuff" method
            var callers = SymbolFinder.FindCallersAsync(method, Solution).Result;

            //SymbolFinder.FindReferencesAsync(null, Solution);
        }
    }
}