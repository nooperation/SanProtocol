using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class InventoryItemUpload : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryItemUpload;

        public string Authorization { get; set; }
        public SanUUID ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }

        public InventoryItemUpload(string authorization, SanUUID itemId, string itemName, string categoryName)
        {
            this.Authorization = authorization;
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.CategoryName = categoryName;
        }

        public InventoryItemUpload(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ItemId = br.ReadSanUUID();
            ItemName = br.ReadSanString();
            CategoryName = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(ItemId);
                    bw.WriteSanString(ItemName);
                    bw.WriteSanString(CategoryName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryItemUpload:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ItemId)} = {ItemId}\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n" +
                   $"  {nameof(CategoryName)} = {CategoryName}\n";
        }
    }
}
