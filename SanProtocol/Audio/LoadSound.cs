﻿namespace SanProtocol.Audio
{
    public class LoadSound : IPacket
    {
        public uint MessageId => Messages.AudioMessages.LoadSound;

        public SanUUID ResourceId { get; set; }

        public LoadSound(SanUUID resourceId)
        {
            ResourceId = resourceId;
        }

        public LoadSound(BinaryReader br)
        {
            ResourceId = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ResourceId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::LoadSound:\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n";
        }
    }
}
