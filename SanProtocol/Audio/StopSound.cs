using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.Audio
{
    public class StopSound : IPacket
    {
        public uint MessageId => Messages.Audio.StopSound;

        public ulong PlayHandleId { get; set; }
        public byte Immediate { get; set; }

        public StopSound(ulong playHandleId, byte immediate)
        {
            this.PlayHandleId = playHandleId;
            this.Immediate = immediate;
        }

        public StopSound(BinaryReader br)
        {
            PlayHandleId = br.ReadUInt64();
            Immediate = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(PlayHandleId);
                    bw.Write(Immediate);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::StopSound:\n" +
                   $"  {nameof(PlayHandleId)} = {PlayHandleId}\n" +
                   $"  {nameof(Immediate)} = {Immediate}\n";
        }
    }
}
