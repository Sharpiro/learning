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
        public void TestOne()
        {
            var workspace = Workspace.Create();
            workspace.Format();
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