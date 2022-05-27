using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ClientSmiteNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientSmiteNotification;

        public string Message { get; set; }

        public ClientSmiteNotification(string message)
        {
            Message = message;
        }

        public ClientSmiteNotification(BinaryReader br)
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
            return $"ClientRegion::ClientSmiteNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
