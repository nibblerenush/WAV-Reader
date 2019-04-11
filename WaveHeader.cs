using System.IO;
using System.Text;

namespace WAV_Reader
{
    internal sealed class WaveHeader
    {
        public WaveHeader(BinaryReader waveBinaryReader)
        {
            _riffChunkDescriptor = new RiffChunkDescriptor
            {
                ChunkID = waveBinaryReader.ReadBytes(TEXT_SIZE),
                ChunkSize = waveBinaryReader.ReadUInt32(),
                Format = waveBinaryReader.ReadBytes(TEXT_SIZE)
            };

            _fmtSubChunk = new FmtSubChunk
            {
                Subchunk1ID = waveBinaryReader.ReadBytes(TEXT_SIZE),
                Subchunk1Size = waveBinaryReader.ReadUInt32(),
                AudioFormat = waveBinaryReader.ReadUInt16(),
                NumChannels = waveBinaryReader.ReadUInt16(),
                SampleRate = waveBinaryReader.ReadUInt32(),
                ByteRate = waveBinaryReader.ReadUInt32(),
                BlockAlign = waveBinaryReader.ReadUInt16(),
                BitsPerSample = waveBinaryReader.ReadUInt16()
            };

            _dataSubChunk = new DataSubChunk
            {
                Subchunk2ID = waveBinaryReader.ReadBytes(TEXT_SIZE),
                Subchunk2Size = waveBinaryReader.ReadUInt32()
            };
        }

        public override string ToString()
        {
            return $"{_riffChunkDescriptor} {_fmtSubChunk} {_dataSubChunk}";
        }

        internal struct RiffChunkDescriptor
        {
            public byte[] ChunkID { get; set; }
            public uint ChunkSize { get; set; }
            public byte[] Format { get; set; }
            public override string ToString()
            {
                return $"\nChunkID = {Encoding.UTF8.GetString(ChunkID)} \nChunkSize = {ChunkSize} \nFormat = {Encoding.UTF8.GetString(Format)}\n";
            }
        }

        internal struct FmtSubChunk
        {
            public byte[] Subchunk1ID { get; set; }
            public uint Subchunk1Size { get; set; }
            public ushort AudioFormat { get; set; }
            public ushort NumChannels { get; set; }
            public uint SampleRate { get; set; }
            public uint ByteRate { get; set; }
            public ushort BlockAlign { get; set; }
            public ushort BitsPerSample { get; set; }
            public override string ToString()
            {
                string part1 = $"\nSubchunk1ID = {Encoding.UTF8.GetString(Subchunk1ID)} \nSubchunk1Size = {Subchunk1Size} \nAudioFormat = {AudioFormat} \nNumChannels = {NumChannels}";
                string part2 = $"\nSampleRate = {SampleRate} \nByteRate = {ByteRate} \nBlockAlign = {BlockAlign} \nBitsPerSample = {BitsPerSample}\n";
                return part1 + part2;
            }
        }

        internal struct DataSubChunk
        {
            public byte[] Subchunk2ID { get; set; }
            public uint Subchunk2Size { get; set; }
            public override string ToString()
            {
                return $"\nSubchunk2ID = {Encoding.UTF8.GetString(Subchunk2ID)} \nSubchunk2Size = {Subchunk2Size}";
            }
        }

        private const int TEXT_SIZE = 4;
        private readonly RiffChunkDescriptor _riffChunkDescriptor;
        private readonly FmtSubChunk _fmtSubChunk;
        private readonly DataSubChunk _dataSubChunk;
    }
}
