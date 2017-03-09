using CommandLine;
using SourcesMerger.Extensions;

namespace SourcesMerger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cliArgument = Parser.Default.ParseOrExitWithHelp<CliArgument>(args);
            new SouceFilesMerger(cliArgument).Merge();
        }
    }
}
