using System;
using System.IO;
using CommandLine;
using SourcesMerger.Extensions;

namespace SourcesMerger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cliArgument = Parser.Default.ParseOrExitWithHelp<CliArgument>(args);
            var souceFilesMerger = new SouceFilesMerger(cliArgument);

            souceFilesMerger.Merge();

            if (cliArgument.Watch)
            {
                var fileWatcher = new FileSystemWatcher
                {
                    Path = cliArgument.SourcesPath,
                    IncludeSubdirectories = true,
                    Filter = "*.cs",
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Security,
                    EnableRaisingEvents = true,
                };
                fileWatcher.Changed += (__, _) =>
                {
                    Console.WriteLine("Change detected!");
                    souceFilesMerger.Merge();
                };
                Console.ReadLine();
            }
        }
    }
}
