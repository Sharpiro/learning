using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RoslynCore.Debugging
{
    public class ScriptDebugger
    {
        private readonly string _source;

        public ScriptDebugger(string source)
        {
            if (string.IsNullOrEmpty(source)) throw new ArgumentNullException(nameof(source));

            _source = source;
        }

        public void Create()
        {
            var assemblyLoader = new InteractiveAssemblyLoader();
            var script = CSharpScript.Create(_source, null, null, assemblyLoader);
            var compilation = script.GetCompilation();
            var diagnostics = compilation.GetDiagnostics().ToList();
            var warnings = diagnostics.Where(d => d.Severity == DiagnosticSeverity.Warning);
            var errors = diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error);
            using (var peStream = new MemoryStream())
            using (var pdbStream = new MemoryStream())
            {
                var emitOptions = new EmitOptions().WithDebugInformationFormat(DebugInformationFormat.PortablePdb);
                var emitResult = compilation.Emit(peStream, pdbStream, null, null, null, emitOptions);

                peStream.Position = 0;
                pdbStream.Position = 0;

                var assembly = assemblyLoader.Invoke<Stream, Stream, Assembly>(
                    "LoadAssemblyFromStream", BindingFlags.NonPublic, peStream, pdbStream);

                var entryPoint = compilation.GetEntryPoint(default(CancellationToken));
                var entryPointType = assembly.GetType(entryPoint.ContainingType.MetadataName, true, false).GetTypeInfo();
                var declaredMethod = entryPointType.GetDeclaredMethod(entryPoint.MetadataName);
                var resultTask =declaredMethod.Invoke<object[], Task<object>>(
                    (object)null, // static invocation
                                new object[] { null, null });
            }
        }
    }
}