using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Audio
{
    public class SetPitch : IPacket
    {
        public uint MessageId => Messages.AudioMessages.SetPitch;

        public uint PlayHandleId { get; set; }
        public uint Pitch { get; set; }

        public SetPitch(uint playHandleId, uint pitch)
        {
            this.PlayHandleId = playHandleId;
            this.Pitch = pitch;
        }

        public SetPitch(BinaryReader br)
        {
            PlayHandleId = br.ReadUInt32();
            Pitch = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(PlayHandleId);
                    bw.Write(Pitch);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::SetPitch:\n" +
                   $"  {nameof(PlayHandleId)} = {PlayHandleId}\n" +
                   $"  {nameof(Pitch)} = {Pitch}\n";
        }
    }
}
