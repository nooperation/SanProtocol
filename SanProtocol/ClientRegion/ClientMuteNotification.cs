using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ClientMuteNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientMuteNotification;

        public string Message { get; set; }

        public ClientMuteNotification(string message)
        {
            Message = message;
        }

        public ClientMuteNotification(BinaryReader br)
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
            return $"ClientRegion::ClientMuteNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
