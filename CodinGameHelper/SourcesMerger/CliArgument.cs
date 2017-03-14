using System;
using System.Text;
using CommandLine;

namespace SourcesMerger
{
    public class CliArgument
    {
        [Option(shortName: 's', longName: "sourcesPath", Required = true, HelpText = "path to your source code")]
        public string SourcesPath { get; set; }

        [Option('o', "outputFile", Required = true, HelpText = "path to your output file")]
        public string OutputFile { get; set; }

        [Option('e', "encoding", Required = false, DefaultValue = EncodingEnum.Default, HelpText = "Encoding of your source code (Default/UTF8/ASCII/Unicode)")]
        public EncodingEnum EncodingEnum { private get; set; }

        public Encoding Encoding => EncodingEnum.ToEncoding();

        [Option('w', "watch", Required = false, DefaultValue = false, HelpText = "enable watch")]
        public bool Watch { get; set; }
    }

}