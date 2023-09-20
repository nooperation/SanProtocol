using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class InventoryItemRevision : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.InventoryItemRevision;

        public string Asset_id { get; set; }
        public string Asset_type { get; set; }
        public uint Asset_hint { get; set; }
        public string Thumbnail_asset_id { get; set; }
        public string License_asset_id { get; set; }
        public List<string> Capabilities { get; set; } = new List<string>();

        public InventoryItemRevision(string asset_id, string asset_type, uint asset_hint, string thumbnail_asset_id, string license_asset_id, List<string> capabilities)
        {
            this.Asset_id = asset_id;
            this.Asset_type = asset_type;
            this.Asset_hint = asset_hint;
            this.Thumbnail_asset_id = thumbnail_asset_id;
            this.License_asset_id = license_asset_id;
            this.Capabilities = capabilities;
        }

        public InventoryItemRevision(BinaryReader br)
        {
            Asset_id = br.ReadSanString();
            Asset_type = br.ReadSanString();
            Asset_hint = br.ReadUInt32();
            Thumbnail_asset_id = br.ReadSanString();
            License_asset_id = br.ReadSanString();
            var numCapabilities = br.ReadUInt32();
            for (var i = 0; i < numCapabilities; ++i)
            {
                var str = br.ReadSanString();
                Capabilities.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Asset_id);
                    bw.WriteSanString(Asset_type);
                    bw.Write(Asset_hint);
                    bw.WriteSanString(Thumbnail_asset_id);
                    bw.WriteSanString(License_asset_id);
                    bw.Write(Capabilities.Count);
                    foreach (var item in Capabilities)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::InventoryItemRevision:\n" +
                   $"  {nameof(Asset_id)} = {Asset_id}\n" +
                   $"  {nameof(Asset_type)} = {Asset_type}\n" +
                   $"  {nameof(Asset_hint)} = {Asset_hint}\n" +
                   $"  {nameof(Thumbnail_asset_id)} = {Thumbnail_asset_id}\n" +
                   $"  {nameof(License_asset_id)} = {License_asset_id}\n" +
                   $"  {nameof(Capabilities)} = {String.Join(',', Capabilities)}\n";
        }
    }
}
