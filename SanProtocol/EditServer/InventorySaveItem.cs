using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class InventorySaveItem : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.InventorySaveItem;

        public string Authorization { get; set; }
        public SanUUID SaveItemRequestId { get; set; }
        public SanUUID ItemToSaveId { get; set; }
        public string AssetId { get; set; }
        public string LicenseAssetName { get; set; }
        public byte AssetState { get; set; }
        public string AssetSerializationTag { get; set; }
        public ulong ResourceVersion { get; set; }

        public InventorySaveItem(string authorization, SanUUID saveItemRequestId, SanUUID itemToSaveId, string assetId, string licenseAssetName, byte assetState, string assetSerializationTag, ulong resourceVersion)
        {
            this.Authorization = authorization;
            this.SaveItemRequestId = saveItemRequestId;
            this.ItemToSaveId = itemToSaveId;
            this.AssetId = assetId;
            this.LicenseAssetName = licenseAssetName;
            this.AssetState = assetState;
            this.AssetSerializationTag = assetSerializationTag;
            this.ResourceVersion = resourceVersion;
        }

        public InventorySaveItem(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            SaveItemRequestId = br.ReadSanUUID();
            ItemToSaveId = br.ReadSanUUID();
            AssetId = br.ReadSanString();
            LicenseAssetName = br.ReadSanString();
            AssetState = br.ReadByte();
            AssetSerializationTag = br.ReadSanString();
            ResourceVersion = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(SaveItemRequestId);
                    bw.Write(ItemToSaveId);
                    bw.WriteSanString(AssetId);
                    bw.WriteSanString(LicenseAssetName);
                    bw.Write(AssetState);
                    bw.WriteSanString(AssetSerializationTag);
                    bw.Write(ResourceVersion);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventorySaveItem:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(SaveItemRequestId)} = {SaveItemRequestId}\n" +
                   $"  {nameof(ItemToSaveId)} = {ItemToSaveId}\n" +
                   $"  {nameof(AssetId)} = {AssetId}\n" +
                   $"  {nameof(LicenseAssetName)} = {LicenseAssetName}\n" +
                   $"  {nameof(AssetState)} = {AssetState}\n" +
                   $"  {nameof(AssetSerializationTag)} = {AssetSerializationTag}\n" +
                   $"  {nameof(ResourceVersion)} = {ResourceVersion}\n";
        }
    }
}
