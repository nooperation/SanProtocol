using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class InventoryModifyItemThumbnailAssetId : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryModifyItemThumbnailAssetId;

        public string Authorization { get; set; }
        public SanUUID ModifyThumbnailRequestId { get; set; }
        public SanUUID ItemToChangeId { get; set; }
        public string NewThumbnailAssetId { get; set; }

        public InventoryModifyItemThumbnailAssetId(string authorization, SanUUID modifyThumbnailRequestId, SanUUID itemToChangeId, string newThumbnailAssetId)
        {
            this.Authorization = authorization;
            this.ModifyThumbnailRequestId = modifyThumbnailRequestId;
            this.ItemToChangeId = itemToChangeId;
            this.NewThumbnailAssetId = newThumbnailAssetId;
        }

        public InventoryModifyItemThumbnailAssetId(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ModifyThumbnailRequestId = br.ReadSanUUID();
            ItemToChangeId = br.ReadSanUUID();
            NewThumbnailAssetId = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(ModifyThumbnailRequestId);
                    bw.Write(ItemToChangeId);
                    bw.WriteSanString(NewThumbnailAssetId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryModifyItemThumbnailAssetId:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ModifyThumbnailRequestId)} = {ModifyThumbnailRequestId}\n" +
                   $"  {nameof(ItemToChangeId)} = {ItemToChangeId}\n" +
                   $"  {nameof(NewThumbnailAssetId)} = {NewThumbnailAssetId}\n";
        }
    }
}
