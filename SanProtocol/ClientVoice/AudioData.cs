using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class AudioData : IPacket
    {
        public uint MessageId => Messages.ClientVoice.AudioData;

        public ulong Sequence { get; set; }
        public ushort Volume { get; set; }
        public byte[] Data { get; set; }

        public AudioData(ulong sequence, ushort volume, byte[] data)
        {
            this.Sequence = sequence;
            this.Volume = volume;
            this.Data = data;
        }

        public AudioData(BinaryReader br)
        {
            Sequence = br.ReadUInt64();
            Volume = br.ReadUInt16();
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
                    bw.Write(Volume);
                    bw.Write(Data.Length);
                    bw.Write(Data);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::AudioData:\n" +
                   $"  {nameof(Sequence)} = {Sequence}\n" +
                   $"  {nameof(Volume)} = {Volume}\n" +
                   $"  {nameof(Data)} = {Data}\n";
        }
    }
}
