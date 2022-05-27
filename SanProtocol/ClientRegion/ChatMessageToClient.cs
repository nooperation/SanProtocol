using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ChatMessageToClient : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ChatMessageToClient;

        public uint FromSessionId { get; set; }
        public uint ToSessionId { get; set; }
        public string Message { get; set; }

        public ChatMessageToClient(uint fromSessionId, uint toSessionId, string message)
        {
            FromSessionId = fromSessionId;
            ToSessionId = toSessionId;
            Message = message;
        }

        public ChatMessageToClient(BinaryReader br)
        {
            FromSessionId = br.ReadUInt32();
            ToSessionId = br.ReadUInt32();
            Message = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(FromSessionId);
                    bw.Write(ToSessionId);
                    bw.WriteSanString(Message);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ChatMessageToClient:\n" +
                   $"  {nameof(FromSessionId)} = {FromSessionId}\n" +
                   $"  {nameof(ToSessionId)} = {ToSessionId}\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
