using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class RelationshipOperation : IPacket
    {
        public uint MessageId => Messages.ClientKafka.RelationshipOperation;

        public SanUUID Other { get; set; }
        public uint Operation { get; set; }

        public RelationshipOperation(SanUUID other, uint operation)
        {
            Other = other;
            Operation = operation;
        }

        public RelationshipOperation(BinaryReader br)
        {
            Other = br.ReadSanUUID();
            Operation = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Other);
                    bw.Write(Operation);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::RelationshipOperation:\n" +
                   $"  {nameof(Other)} = {Other}\n" +
                   $"  {nameof(Operation)} = {Operation}\n";
        }
    }

}
