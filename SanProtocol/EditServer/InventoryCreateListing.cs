using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class InventoryCreateListing : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryCreateListing;

        public string Authorization { get; set; }
        public SanUUID ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string BundleName { get; set; }

        public InventoryCreateListing(string authorization, SanUUID itemId, string itemName, string categoryName, string bundleName)
        {
            this.Authorization = authorization;
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.CategoryName = categoryName;
            this.BundleName = bundleName;
        }

        public InventoryCreateListing(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ItemId = br.ReadSanUUID();
            ItemName = br.ReadSanString();
            CategoryName = br.ReadSanString();
            BundleName = br.ReadSanString();
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
                    bw.WriteSanString(BundleName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryCreateListing:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ItemId)} = {ItemId}\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n" +
                   $"  {nameof(CategoryName)} = {CategoryName}\n" +
                   $"  {nameof(BundleName)} = {BundleName}\n";
        }
    }
}
