using System;
using System.Collections.Generic;
using System.IO;

namespace WAV_Reader
{
    internal sealed class WaveParser
    {
        public WaveParser(List<string> waveFilePaths)
        {
            _waveFilePath = waveFilePaths;
        }

        public void Parse()
        {
            if (!CheckFilesExist())
            {
                return;
            }

            foreach (string waveFilePath in _waveFilePath)
            {
                FileStream waveFileStream = new FileStream(waveFilePath, FileMode.Open);
                BinaryReader waveFileBinaryReader = new BinaryReader(waveFileStream);

                try
                {
                    WaveHeader waveHeader = new WaveHeader(waveFileBinaryReader);
                    ContentPrinter(waveFilePath, waveHeader);
                    Console.ReadLine();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return;
                }
            }
        }

        private bool CheckFilesExist()
        {
            if (_waveFilePath.Count != 0)
            {
                foreach (string waveFilePath in _waveFilePath)
                {
                    if (!File.Exists(waveFilePath))
                    {
                        Console.WriteLine("File \"{0}\" does not exist!", waveFilePath);
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("List of files is empty!");
                return false;
            }
            return true;
        }

        private void ContentPrinter(string waveFilePath, WaveHeader waveHeader)
        {
            Console.WriteLine("+++++");
            Console.WriteLine($"File: {waveFilePath}");
            Console.WriteLine("+++++");
            Console.WriteLine($"Header content: {waveHeader}");
            Console.WriteLine("-----");
        }

        readonly List<string> _waveFilePath;
    }
}
