using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class FriendRequest : IPacket
    {
        public uint MessageId => Messages.ClientKafka.FriendRequest;

        public ulong Offset { get; set; }
        public SanUUID FromPersonaId { get; set; }
        public SanUUID ToPersonaId { get; set; }
        public string Timestamp { get; set; }
        public string FromSignature { get; set; }

        public FriendRequest(ulong offset, SanUUID fromPersonaId, SanUUID toPersonaId, string timestamp, string fromSignature)
        {
            Offset = offset;
            FromPersonaId = fromPersonaId;
            ToPersonaId = toPersonaId;
            Timestamp = timestamp;
            FromSignature = fromSignature;
        }

        public FriendRequest(BinaryReader br)
        {
            Offset = br.ReadUInt64();
            FromPersonaId = br.ReadSanUUID();
            ToPersonaId = br.ReadSanUUID();
            Timestamp = br.ReadSanString();
            FromSignature = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Offset);
                    bw.Write(FromPersonaId);
                    bw.Write(ToPersonaId);
                    bw.WriteSanString(Timestamp);
                    bw.WriteSanString(FromSignature);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::FriendRequest:\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(FromPersonaId)} = {FromPersonaId}\n" +
                   $"  {nameof(ToPersonaId)} = {ToPersonaId}\n" +
                   $"  {nameof(Timestamp)} = {Timestamp}\n" +
                   $"  {nameof(FromSignature)} = {FromSignature}\n";
        }
    }

}
