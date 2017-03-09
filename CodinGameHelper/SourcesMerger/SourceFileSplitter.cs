using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoreLinq;

namespace SourcesMerger
{
    public class SourceFileSplitter
    {
        public IEnumerable<UsingDirectiveSyntax> UsingDirectiveSyntaxNodes { get;  }
        public SyntaxNode CodeWithoutUsingDirectives { get; }

        private SourceFileSplitter(SyntaxTree syntaxTree)
        {
            var descendantNodes = syntaxTree.GetRoot().DescendantNodes();

            var usingDirectiveSyntaxs = descendantNodes.OfType<UsingDirectiveSyntax>();
            UsingDirectiveSyntaxNodes = usingDirectiveSyntaxs.DistinctBy(u => u.ToString()).ToList();

            CodeWithoutUsingDirectives = syntaxTree.GetRoot().RemoveNodes(usingDirectiveSyntaxs, SyntaxRemoveOptions.KeepNoTrivia);
        }

        public static SourceFileSplitter ForPath(string sourceFile, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.Default;

            var content = ReadAllText(sourceFile, encoding);
            var syntaxTree = CSharpSyntaxTree.ParseText(content, CSharpParseOptions.Default, sourceFile, encoding);
            return new SourceFileSplitter(syntaxTree);
        }

        public static SourceFileSplitter ForString(string content)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(content, CSharpParseOptions.Default, string.Empty, Encoding.Default);
            return new SourceFileSplitter(syntaxTree);
        }

        private static string ReadAllText(string path, Encoding encoding)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, encoding))
            {
                return sr.ReadToEnd();
            }
        }
    }
}