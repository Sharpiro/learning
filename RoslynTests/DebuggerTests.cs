using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoslynCore.Debugging;

namespace RoslynTests
{
    [TestClass]
    public class DebuggerTests
    {
        [TestMethod]
        public void DebugTest()
        {
            const string source = "Console.WriteLine(\"Hello, World!\");";
            var debugger = new ScriptDebugger(source);
            debugger.Create();
        }
    }
}