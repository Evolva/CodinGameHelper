using System.Linq;
using NFluent;
using NUnit.Framework;
using SourcesMerger;

namespace SourcesMergerTests
{
    public class SourceFileSplitterTests
    {
        [Test]
        public void Must_Extract_Using_Directives_From_Source_Code_When_Calling_UsingDirectiveSyntaxNodes()
        {
            const string expected =
@"using NUnit.Framework;
using NFluent;
";
            var value = string.Join(string.Empty, _sourceFileSplitter.UsingDirectiveSyntaxNodes.Select(x => x.ToFullString().TrimStart()));
            Check.That(value).IsEqualTo(expected);
        }

        [Test]
        public void Must_Remove_Using_Directives_From_Source_Code_When_Calling_CodeWithoutUsingDirectives()
        {
            const string expected =
@"
namespace SourcesMergerTests
{

    public class SourceFileSplitterTests
    {
        [Test]
        public void Can_Extract_Using_Statements_From_Source_Code()
        {
        }
    }
}
";
            var value = _sourceFileSplitter.CodeWithoutUsingDirectives.ToFullString();
            Check.That(value).IsEqualTo(expected);
        }

        private SourceFileSplitter _sourceFileSplitter;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            const string sourceCode =
@"using NUnit.Framework;
using NFluent;

namespace SourcesMergerTests
{
    using NFluent;

    public class SourceFileSplitterTests
    {
        [Test]
        public void Can_Extract_Using_Statements_From_Source_Code()
        {
        }
    }
}
";
            _sourceFileSplitter = SourceFileSplitter.ForString(sourceCode);
        }
    }
}
