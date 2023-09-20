using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class RelationshipTable : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.RelationshipTable;

        public SanUUID Other { get; set; }
        public byte FromSelf { get; set; }
        public byte FromOther { get; set; }
        public uint Status { get; set; }

        public RelationshipTable(SanUUID other, byte fromSelf, byte fromOther, uint status)
        {
            Other = other;
            FromSelf = fromSelf;
            FromOther = fromOther;
            Status = status;
        }

        public RelationshipTable(BinaryReader br)
        {
            Other = br.ReadSanUUID();
            FromSelf = br.ReadByte();
            FromOther = br.ReadByte();
            Status = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Other);
                    bw.Write(FromSelf);
                    bw.Write(FromOther);
                    bw.Write(Status);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::RelationshipTable:\n" +
                   $"  {nameof(Other)} = {Other}\n" +
                   $"  {nameof(FromSelf)} = {FromSelf}\n" +
                   $"  {nameof(FromOther)} = {FromOther}\n" +
                   $"  {nameof(Status)} = {Status}\n";
        }
    }
}
