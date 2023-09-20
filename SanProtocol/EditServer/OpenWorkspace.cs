using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class OpenWorkspace : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.OpenWorkspace;

        public SanUUID WorldSourceInventoryItemId { get; set; }
        public string WorldSourceResourceId { get; set; }

        public OpenWorkspace(SanUUID worldSourceInventoryItemId, string worldSourceResourceId)
        {
            this.WorldSourceInventoryItemId = worldSourceInventoryItemId;
            this.WorldSourceResourceId = worldSourceResourceId;
        }

        public OpenWorkspace(BinaryReader br)
        {
            WorldSourceInventoryItemId = br.ReadSanUUID();
            WorldSourceResourceId = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(WorldSourceInventoryItemId);
                    bw.WriteSanString(WorldSourceResourceId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::OpenWorkspace:\n" +
                   $"  {nameof(WorldSourceInventoryItemId)} = {WorldSourceInventoryItemId}\n" +
                   $"  {nameof(WorldSourceResourceId)} = {WorldSourceResourceId}\n";
        }
    }
}
