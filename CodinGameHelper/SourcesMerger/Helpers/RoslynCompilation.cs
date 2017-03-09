using System;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

namespace SourcesMerger.Helpers
{
    public class RoslynCompilation : IDisposable
    {
        public bool Success { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public MemoryStream MemoryStream { get; }

        public RoslynCompilation(EmitResult emitResult, MemoryStream memoryStream)
        {
            Success = emitResult.Success;
            Diagnostics = emitResult.Diagnostics;
            MemoryStream = memoryStream;
        }

        public void Dispose()
        {
            MemoryStream?.Dispose();
        }
    }
}