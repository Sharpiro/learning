using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoslynCore;
using System;

namespace RoslynTests
{
    /// <summary>
    /// requires the following dlls in executing project as well
    /// Microsoft.CodeAnalysis.CSharp
    /// Microsoft.CodeAnalysis.CSharp.Workspaces
    /// </summary>
    [TestClass]
    public class WorkspaceTests
    {
        [TestMethod]
        public void FormatTests()
        {
            var workspace = Workspace.Create();
            workspace.Format();
        }

        [TestMethod]
        public void FindSymbolsTests()
        {
            var workspace = Workspace.Create();
            workspace.FindSymbols();
        }
    }

    public class Temp
    {
        private readonly object _dependency;

        public Temp(object dependency)
        {
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));

            _dependency = dependency;
        }
    }
}