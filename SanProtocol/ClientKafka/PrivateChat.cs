using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class PrivateChat : IPacket
    {
        public uint MessageId => Messages.ClientKafka.PrivateChat;

        public ulong Offset { get; set; }
        public SanUUID FromPersonaId { get; set; }
        public SanUUID ToPersonaId { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }

        public PrivateChat(ulong offset, SanUUID fromPersonaId, SanUUID toPersonaId, string message, long timestamp)
        {
            Offset = offset;
            FromPersonaId = fromPersonaId;
            ToPersonaId = toPersonaId;
            Message = message;
            Timestamp = timestamp;
        }

        public PrivateChat(BinaryReader br)
        {
            Offset = br.ReadUInt64();
            FromPersonaId = br.ReadSanUUID();
            ToPersonaId = br.ReadSanUUID();
            Message = br.ReadSanString();
            Timestamp = br.ReadInt64();
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
                    bw.WriteSanString(Message);
                    bw.Write(Timestamp);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::PrivateChat:\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(FromPersonaId)} = {FromPersonaId}\n" +
                   $"  {nameof(ToPersonaId)} = {ToPersonaId}\n" +
                   $"  {nameof(Message)} = {Message}\n" +
                   $"  {nameof(Timestamp)} = {Timestamp}\n";
        }
    }

}
