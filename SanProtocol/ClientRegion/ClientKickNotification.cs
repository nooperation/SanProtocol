using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ClientKickNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientKickNotification;

        public string Message { get; set; }

        public ClientKickNotification(string message)
        {
            Message = message;
        }

        public ClientKickNotification(BinaryReader br)
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
            return $"ClientRegion::ClientKickNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
