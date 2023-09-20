using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class PrivateChatStatusLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.PrivateChatStatusLoaded;

        public ulong Offset { get; set; }

        public PrivateChatStatusLoaded(ulong offset)
        {
            Offset = offset;
        }

        public PrivateChatStatusLoaded(BinaryReader br)
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
            return $"ClientKafka::PrivateChatStatusLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }
}
