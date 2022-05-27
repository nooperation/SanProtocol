using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class FriendResponseStatus : IPacket
    {
        public uint MessageId => Messages.ClientKafka.FriendResponseStatus;

        public ulong Offset { get; set; }
        public uint Status { get; set; }

        public FriendResponseStatus(ulong offset, uint status)
        {
            Offset = offset;
            Status = status;
        }

        public FriendResponseStatus(BinaryReader br)
        {
            Offset = br.ReadUInt64();
            Status = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Offset);
                    bw.Write(Status);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::FriendResponseStatus:\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(Status)} = {Status}\n";
        }
    }

}
