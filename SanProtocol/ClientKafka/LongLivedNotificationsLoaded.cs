﻿namespace SanProtocol.ClientKafka
{
    public class LongLivedNotificationsLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.LongLivedNotificationsLoaded;

        public ulong Offset { get; set; }

        public LongLivedNotificationsLoaded(ulong offset)
        {
            Offset = offset;
        }

        public LongLivedNotificationsLoaded(BinaryReader br)
        {
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::LongLivedNotificationsLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
