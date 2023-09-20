using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Audio
{
    public class SetLoudness : IPacket
    {
        public uint MessageId => Messages.AudioMessages.SetLoudness;

        public uint PlayHandleId { get; set; }
        public uint Loudness { get; set; }

        public SetLoudness(uint playHandleId, uint loudness)
        {
            this.PlayHandleId = playHandleId;
            this.Loudness = loudness;
        }

        public SetLoudness(BinaryReader br)
        {
            PlayHandleId = br.ReadUInt32();
            Loudness = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(PlayHandleId);
                    bw.Write(Loudness);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::SetLoudness:\n" +
                   $"  {nameof(PlayHandleId)} = {PlayHandleId}\n" +
                   $"  {nameof(Loudness)} = {Loudness}\n";
        }
    }
}
