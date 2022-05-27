using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class InventoryDeleteItem : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryDeleteItem;

        public string Authorization { get; set; }
        public SanUUID DeleteRequestId { get; set; }
        public SanUUID ItemToDeleteId { get; set; }

        public InventoryDeleteItem(string authorization, SanUUID deleteRequestId, SanUUID itemToDeleteId)
        {
            this.Authorization = authorization;
            this.DeleteRequestId = deleteRequestId;
            this.ItemToDeleteId = itemToDeleteId;
        }

        public InventoryDeleteItem(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            DeleteRequestId = br.ReadSanUUID();
            ItemToDeleteId = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(DeleteRequestId);
                    bw.Write(ItemToDeleteId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryDeleteItem:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(DeleteRequestId)} = {DeleteRequestId}\n" +
                   $"  {nameof(ItemToDeleteId)} = {ItemToDeleteId}\n";
        }
    }
}
