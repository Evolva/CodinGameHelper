using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SourcesMerger.Helpers
{
    public class RoslynCompilationHelper
    {
        private CSharpCompilation _compilation;

        public RoslynCompilationHelper(string assemblyName, OutputKind outputKind)
        {
            _compilation = CSharpCompilation.Create(assemblyName, options: new CSharpCompilationOptions(outputKind));
        }

        public RoslynCompilationHelper AddReferences(params MetadataReference[] references)
        {
            _compilation = _compilation.AddReferences(references);
            return this;
        }

        public RoslynCompilationHelper AddSyntaxTrees(params SyntaxTree[] syntaxTrees)
        {
            _compilation = _compilation.AddSyntaxTrees(syntaxTrees);
            return this;
        }

        public RoslynCompilation TryToCompile()
        {
            var memoryStream = new MemoryStream();
            var result = _compilation.Emit(memoryStream);

            if (result.Success)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                return new RoslynCompilation(result, memoryStream);
            }
            return new RoslynCompilation(result, null);
        }
    }
}