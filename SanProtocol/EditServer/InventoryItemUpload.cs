namespace SanProtocol.EditServer
{
    public class InventoryItemUpload : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.InventoryItemUpload;

        public string Authorization { get; set; }
        public SanUUID ItemId { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }

        public InventoryItemUpload(string authorization, SanUUID itemId, string itemName, string categoryName)
        {
            Authorization = authorization;
            ItemId = itemId;
            ItemName = itemName;
            CategoryName = categoryName;
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
