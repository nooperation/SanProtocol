using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class FriendResponse : IPacket
    {
        public uint MessageId => Messages.ClientKafka.FriendResponse;

        public ulong Offset { get; set; }
        public SanUUID FromPersonaId { get; set; }
        public SanUUID ToPersonaId { get; set; }
        public string Timestamp { get; set; }
        public string FromSignature { get; set; }
        public uint Response { get; set; }
        public string ToSignature { get; set; }

        public FriendResponse(ulong offset, SanUUID fromPersonaId, SanUUID toPersonaId, string timestamp, string fromSignature, uint response, string toSignature)
        {
            Offset = offset;
            FromPersonaId = fromPersonaId;
            ToPersonaId = toPersonaId;
            Timestamp = timestamp;
            FromSignature = fromSignature;
            Response = response;
            ToSignature = toSignature;
        }

        public FriendResponse(BinaryReader br)
        {
            Offset = br.ReadUInt64();
            FromPersonaId = br.ReadSanUUID();
            ToPersonaId = br.ReadSanUUID();
            Timestamp = br.ReadSanString();
            FromSignature = br.ReadSanString();
            Response = br.ReadUInt32();
            ToSignature = br.ReadSanString();
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
                    bw.Write(Response);
                    bw.WriteSanString(ToSignature);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::FriendResponse:\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(FromPersonaId)} = {FromPersonaId}\n" +
                   $"  {nameof(ToPersonaId)} = {ToPersonaId}\n" +
                   $"  {nameof(Timestamp)} = {Timestamp}\n" +
                   $"  {nameof(FromSignature)} = {FromSignature}\n" +
                   $"  {nameof(Response)} = {Response}\n" +
                   $"  {nameof(ToSignature)} = {ToSignature}\n";
        }
    }

}
