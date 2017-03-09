using CommandLine;

namespace SourcesMerger.Extensions
{
    public static class ParserExtensions
    {
        public static T ParseOrExitWithHelp<T>(this Parser parser, string[] args)
            where T : new()
        {
            var cliArgs = new T();
            parser.ParseArgumentsStrict(args, cliArgs);
            return cliArgs;
        }
    }
}