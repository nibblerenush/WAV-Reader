using CommandLine;
using System.Collections.Generic;

namespace WAV_Reader
{
    public sealed class Program
    {
        private sealed class Options
        {
            [Value(0, Required = true, HelpText = "List of input files")]
            public IEnumerable<string> FilePaths { get; set; }
        }
        public static void Main(string[] args)
        {
            List<string> waveFilePaths = new List<string>();

            Parser.Default.ParseArguments<Options>(args).WithParsed(option =>
            {
                foreach (string filePath in option.FilePaths)
                {
                    waveFilePaths.Add(filePath);
                }
            });

            WaveParser waveParser = new WaveParser(waveFilePaths);
            waveParser.Parse();
        }
    }
}
