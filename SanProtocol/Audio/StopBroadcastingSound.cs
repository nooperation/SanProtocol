﻿namespace SanProtocol.Audio
{
    public class StopBroadcastingSound : IPacket
    {
        public uint MessageId => Messages.AudioMessages.StopBroadcastingSound;

        public ulong PlayHandleId { get; set; }

        public StopBroadcastingSound(ulong playHandleId)
        {
            PlayHandleId = playHandleId;
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
