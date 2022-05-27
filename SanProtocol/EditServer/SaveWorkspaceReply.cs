using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class SaveWorkspaceReply : IPacket
    {
        public uint MessageId => Messages.EditServer.SaveWorkspaceReply;

        public byte Success { get; set; }
        public string ItemInventoryId { get; set; }
        public string ItemResourceId { get; set; }
        public string ItemName { get; set; }

        public SaveWorkspaceReply(byte success, string itemInventoryId, string itemResourceId, string itemName)
        {
            this.Success = success;
            this.ItemInventoryId = itemInventoryId;
            this.ItemResourceId = itemResourceId;
            this.ItemName = itemName;
        }

        public SaveWorkspaceReply(BinaryReader br)
        {
            Success = br.ReadByte();
            ItemInventoryId = br.ReadSanString();
            ItemResourceId = br.ReadSanString();
            ItemName = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Success);
                    bw.WriteSanString(ItemInventoryId);
                    bw.WriteSanString(ItemResourceId);
                    bw.WriteSanString(ItemName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::SaveWorkspaceReply:\n" +
                   $"  {nameof(Success)} = {Success}\n" +
                   $"  {nameof(ItemInventoryId)} = {ItemInventoryId}\n" +
                   $"  {nameof(ItemResourceId)} = {ItemResourceId}\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n";
        }
    }
}
