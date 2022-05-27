using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientVoice
{
    public class SpeechGraphicsData : IPacket
    {
        public uint MessageId => Messages.ClientVoice.SpeechGraphicsData;

        public ulong Sequence { get; set; }
        public byte[] Data { get; set; }

        public SpeechGraphicsData(ulong sequence, byte[] data)
        {
            this.Sequence = sequence;
            this.Data = data;
        }

        public SpeechGraphicsData(BinaryReader br)
        {
            Sequence = br.ReadUInt64();
            var dataLength = br.ReadInt32();
            Data = br.ReadBytes(dataLength);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Sequence);
                    bw.Write(Data.Length);
                    bw.Write(Data);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::SpeechGraphicsData:\n" +
                   $"  {nameof(Sequence)} = {Sequence}\n" +
                   $"  {nameof(Data)} = {Data}\n";
        }
    }
}
