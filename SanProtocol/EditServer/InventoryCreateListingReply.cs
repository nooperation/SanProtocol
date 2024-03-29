﻿namespace SanProtocol.EditServer
{
    public class InventoryCreateListingReply : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.InventoryCreateListingReply;

        public byte CanBeListed { get; set; }
        public SanUUID ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }

        public InventoryCreateListingReply(byte canBeListed, SanUUID itemId, string itemName, string categoryName)
        {
            CanBeListed = canBeListed;
            ItemId = itemId;
            ItemName = itemName;
            CategoryName = categoryName;
        }

        public InventoryCreateListingReply(BinaryReader br)
        {
            CanBeListed = br.ReadByte();
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
                    bw.Write(CanBeListed);
                    bw.Write(ItemId);
                    bw.WriteSanString(ItemName);
                    bw.WriteSanString(CategoryName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryCreateListingReply:\n" +
                   $"  {nameof(CanBeListed)} = {CanBeListed}\n" +
                   $"  {nameof(ItemId)} = {ItemId}\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n" +
                   $"  {nameof(CategoryName)} = {CategoryName}\n";
        }
    }
}
