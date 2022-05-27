using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ChatMessageToServer : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ChatMessageToServer;

        public uint ToSessionId { get; set; }
        public string Message { get; set; }

        public ChatMessageToServer(uint toSessionId, string message)
        {
            ToSessionId = toSessionId;
            Message = message;
        }

        public ChatMessageToServer(BinaryReader br)
        {
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
                    bw.Write(ToSessionId);
                    bw.WriteSanString(Message);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ChatMessageToServer:\n" +
                   $"  {nameof(ToSessionId)} = {ToSessionId}\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
