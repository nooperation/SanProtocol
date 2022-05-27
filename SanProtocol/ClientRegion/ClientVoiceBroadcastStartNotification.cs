using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ClientVoiceBroadcastStartNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientVoiceBroadcastStartNotification;

        public string Message { get; set; }

        public ClientVoiceBroadcastStartNotification(string message)
        {
            Message = message;
        }

        public ClientVoiceBroadcastStartNotification(BinaryReader br)
        {
            Message = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Message);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientVoiceBroadcastStartNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
