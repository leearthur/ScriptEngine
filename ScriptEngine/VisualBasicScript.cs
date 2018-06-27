using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis;
using System.IO;
using System.Reflection;
using System;

namespace ScriptEngine
{
    public class VisualBasicScript
    {
        public static T Evaluate<T>(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            return Run<T>(code);
        }

        public static T Run<T>(string code)
        {
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var tree = VisualBasicSyntaxTree.ParseText(PlaceCodeInsideBlock(code));
            var options = new VisualBasicCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

            var comp = VisualBasicCompilation.Create("InMemoryAssembly")
                .WithOptions(options)
                .AddReferences(mscorlib)
                .AddSyntaxTrees(tree);

            using (var stream = new MemoryStream())
            {
                var response = comp.Emit(stream);
                if (response.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(stream.ToArray());

                    Type type = assembly.GetType("Container");
                    return (T)type.InvokeMember("Execute", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, null);
                }
            }

            return default(T);
        }

        private static string PlaceCodeInsideBlock(string code)
        {
            return
$@"Public Class Container
    Public Shared Function Execute() As Object
        {code}
    End Function
End Class";
        }
    }
}
