using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class LongLivedNotification : IPacket
    {
        public uint MessageId => Messages.ClientKafka.LongLivedNotification;

        public SanUUID Id { get; set; }
        public uint Type { get; set; }
        public string Message { get; set; }
        public ulong Timestamp { get; set; }

        public LongLivedNotification(SanUUID id, uint type, string message, ulong timestamp)
        {
            Id = id;
            Type = type;
            Message = message;
            Timestamp = timestamp;
        }

        public LongLivedNotification(BinaryReader br)
        {
            Id = br.ReadSanUUID();
            Type = br.ReadUInt32();
            Message = br.ReadSanString();
            Timestamp = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Id);
                    bw.Write(Type);
                    bw.WriteSanString(Message);
                    bw.Write(Timestamp);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::LongLivedNotification:\n" +
                   $"  {nameof(Id)} = {Id}\n" +
                   $"  {nameof(Type)} = {Type}\n" +
                   $"  {nameof(Message)} = {Message}\n" +
                   $"  {nameof(Timestamp)} = {Timestamp}\n";
        }
    }

}
