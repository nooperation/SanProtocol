using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class RelationshipTableLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.RelationshipTableLoaded;

        public ulong Offset { get; set; }

        public RelationshipTableLoaded(ulong offset)
        {
            Offset = offset;
        }

        public RelationshipTableLoaded(BinaryReader br)
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
            return $"ClientKafka::RelationshipTableLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
