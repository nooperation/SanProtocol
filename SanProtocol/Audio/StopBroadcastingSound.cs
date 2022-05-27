using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.Audio
{
    public class StopBroadcastingSound : IPacket
    {
        public uint MessageId => Messages.Audio.StopBroadcastingSound;

        public ulong PlayHandleId { get; set; }

        public StopBroadcastingSound(ulong playHandleId)
        {
            this.PlayHandleId = playHandleId;
        }

        public StopBroadcastingSound(BinaryReader br)
        {
            PlayHandleId = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(PlayHandleId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::StopBroadcastingSound:\n" +
                   $"  {nameof(PlayHandleId)} = {PlayHandleId}\n";
        }
    }
}
