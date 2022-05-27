using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ClientRuntimeInventoryUpdatedNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientRuntimeInventoryUpdatedNotification;

        public string Message { get; set; }

        public ClientRuntimeInventoryUpdatedNotification(string message)
        {
            Message = message;
        }

        public ClientRuntimeInventoryUpdatedNotification(BinaryReader br)
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
            return $"ClientRegion::ClientRuntimeInventoryUpdatedNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
