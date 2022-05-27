using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class InventoryCreateItem : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryCreateItem;

        public string Authorization { get; set; }
        public SanUUID CreateRequestId { get; set; }
        public string ItemName { get; set; }
        public string SsetId { get; set; }
        public string LicenseAssetName { get; set; }
        public string InventoryTag { get; set; }
        public string AssetSerializationTag { get; set; }
        public string ThumbnailAssetId { get; set; }
        public List<string> AdditionalCapabilities { get; set; } = new List<string>();
        public ulong ResourceVersion { get; set; }
        public byte AssetState { get; set; }

        public InventoryCreateItem(string authorization, SanUUID createRequestId, string itemName, string ssetId, string licenseAssetName, string inventoryTag, string assetSerializationTag, string thumbnailAssetId, List<string> additionalCapabilities, ulong resourceVersion, byte assetState)
        {
            this.Authorization = authorization;
            this.CreateRequestId = createRequestId;
            this.ItemName = itemName;
            this.SsetId = ssetId;
            this.LicenseAssetName = licenseAssetName;
            this.InventoryTag = inventoryTag;
            this.AssetSerializationTag = assetSerializationTag;
            this.ThumbnailAssetId = thumbnailAssetId;
            this.AdditionalCapabilities = additionalCapabilities;
            this.ResourceVersion = resourceVersion;
            this.AssetState = assetState;
        }

        public InventoryCreateItem(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            CreateRequestId = br.ReadSanUUID();
            ItemName = br.ReadSanString();
            SsetId = br.ReadSanString();
            LicenseAssetName = br.ReadSanString();
            InventoryTag = br.ReadSanString();
            AssetSerializationTag = br.ReadSanString();
            ThumbnailAssetId = br.ReadSanString();
            var numAdditionalCapabilities = br.ReadUInt32();
            for (var i = 0; i < numAdditionalCapabilities; ++i)
            {
                var str = br.ReadSanString();
                AdditionalCapabilities.Add(str);
            }
            ResourceVersion = br.ReadUInt64();
            AssetState = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(CreateRequestId);
                    bw.WriteSanString(ItemName);
                    bw.WriteSanString(SsetId);
                    bw.WriteSanString(LicenseAssetName);
                    bw.WriteSanString(InventoryTag);
                    bw.WriteSanString(AssetSerializationTag);
                    bw.WriteSanString(ThumbnailAssetId);
                    bw.Write(AdditionalCapabilities.Count);
                    foreach (var item in AdditionalCapabilities)
                    {
                        bw.WriteSanString(item);
                    }
                    bw.Write(ResourceVersion);
                    bw.Write(AssetState);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryCreateItem:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(CreateRequestId)} = {CreateRequestId}\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n" +
                   $"  {nameof(SsetId)} = {SsetId}\n" +
                   $"  {nameof(LicenseAssetName)} = {LicenseAssetName}\n" +
                   $"  {nameof(InventoryTag)} = {InventoryTag}\n" +
                   $"  {nameof(AssetSerializationTag)} = {AssetSerializationTag}\n" +
                   $"  {nameof(ThumbnailAssetId)} = {ThumbnailAssetId}\n" +
                   $"  {nameof(AdditionalCapabilities)} = {String.Join(',', AdditionalCapabilities)}\n" +
                   $"  {nameof(ResourceVersion)} = {ResourceVersion}\n" +
                   $"  {nameof(AssetState)} = {AssetState}\n";
        }
    }
}
