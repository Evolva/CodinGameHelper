using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MoreLinq;
using SourcesMerger.Helpers;

namespace SourcesMerger
{
    public class SouceFilesMerger
    {
        private readonly CliArgument _cliArgument;

        public SouceFilesMerger(CliArgument cliArgument)
        {
            _cliArgument = cliArgument;
        }

        public void Merge()
        {
            var encoding = _cliArgument.Encoding;

            var sourcesFiles =
                Directory.GetFiles(_cliArgument.SourcesPath, "*.cs", SearchOption.AllDirectories)
                    .Where(x => Path.GetFileName(x) != "AssemblyInfo.cs");

            var splitters = sourcesFiles.Select(sourceFile => SourceFileSplitter.ForPath(sourceFile)).ToList();

            var usingsDirectivesCode = string.Join(string.Empty,
                splitters
                    .SelectMany(x => x.UsingDirectiveSyntaxNodes)
                    .DistinctBy(x => x.ToString())
                    .Select(x => x.ToFullString()));

            var codeWithoutUsingDirectives = string.Join(string.Empty, splitters.Select(x => x.CodeWithoutUsingDirectives.ToFullString()));

            var output = usingsDirectivesCode + codeWithoutUsingDirectives;

            var outputFileSyntaxTree = CSharpSyntaxTree.ParseText(output, CSharpParseOptions.Default, _cliArgument.OutputFile, encoding);

            var roslynCompilation = new RoslynCompilationHelper("sourcesMerger", OutputKind.ConsoleApplication)
                .AddReferences(
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),     //mscorlib.dll
                    MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location), //System.Core.dll
                    MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location))        //System.dll
                .AddSyntaxTrees(outputFileSyntaxTree)
                .TryToCompile();

            if (roslynCompilation.Success)
            {
                File.WriteAllText(_cliArgument.OutputFile, output);
            }
            else
            {
                Console.WriteLine(">>> ERROR <<<");
                foreach (var diagError in roslynCompilation.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error))
                {
                    Console.WriteLine(diagError);
                }
            }
        }
    }
}