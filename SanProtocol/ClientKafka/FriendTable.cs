using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class FriendTable : IPacket
    {
        public uint MessageId => Messages.ClientKafka.FriendTable;

        public SanUUID FromPersonaId { get; set; }
        public SanUUID ToPersonaId { get; set; }
        public uint Status { get; set; }

        public FriendTable(SanUUID fromPersonaId, SanUUID toPersonaId, uint status)
        {
            FromPersonaId = fromPersonaId;
            ToPersonaId = toPersonaId;
            Status = status;
        }

        public FriendTable(BinaryReader br)
        {
            FromPersonaId = br.ReadSanUUID();
            ToPersonaId = br.ReadSanUUID();
            Status = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(FromPersonaId);
                    bw.Write(ToPersonaId);
                    bw.Write(Status);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::FriendTable:\n" +
                   $"  {nameof(FromPersonaId)} = {FromPersonaId}\n" +
                   $"  {nameof(ToPersonaId)} = {ToPersonaId}\n" +
                   $"  {nameof(Status)} = {Status}\n";
        }
    }

}
