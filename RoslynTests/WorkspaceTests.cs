using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoslynCore;

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
        public Temp(string dependency)
        {

        }
    }
}