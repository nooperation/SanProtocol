using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class FriendRequestLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.FriendRequestLoaded;

        public ulong Offset { get; set; }

        public FriendRequestLoaded(ulong offset)
        {
            Offset = offset;
        }

        public FriendRequestLoaded(BinaryReader br)
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
            return $"ClientKafka::FriendRequestLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
