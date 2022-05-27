using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class InventoryModifyItemCapabilities : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryModifyItemCapabilities;

        public string Authorization { get; set; }
        public SanUUID ModifyCapabilitiesRequestId { get; set; }
        public SanUUID ItemToChangeId { get; set; }
        public List<string> CapabilitiesToAdd { get; set; } = new List<string>();
        public List<string> CapabilitiesToRemove { get; set; } = new List<string>();

        public InventoryModifyItemCapabilities(string authorization, SanUUID modifyCapabilitiesRequestId, SanUUID itemToChangeId, List<string> capabilitiesToAdd, List<string> capabilitiesToRemove)
        {
            this.Authorization = authorization;
            this.ModifyCapabilitiesRequestId = modifyCapabilitiesRequestId;
            this.ItemToChangeId = itemToChangeId;
            this.CapabilitiesToAdd = capabilitiesToAdd;
            this.CapabilitiesToRemove = capabilitiesToRemove;
        }

        public InventoryModifyItemCapabilities(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ModifyCapabilitiesRequestId = br.ReadSanUUID();
            ItemToChangeId = br.ReadSanUUID();
            var numCapabilitiesToAdd = br.ReadUInt32();
            for (var i = 0; i < numCapabilitiesToAdd; ++i)
            {
                var str = br.ReadSanString();
                CapabilitiesToAdd.Add(str);
            }
            var numCapabilitiesToRemove = br.ReadUInt32();
            for (var i = 0; i < numCapabilitiesToRemove; ++i)
            {
                var str = br.ReadSanString();
                CapabilitiesToRemove.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(ModifyCapabilitiesRequestId);
                    bw.Write(ItemToChangeId);
                    bw.Write(CapabilitiesToAdd.Count);
                    foreach (var item in CapabilitiesToAdd)
                    {
                        bw.WriteSanString(item);
                    }
                    bw.Write(CapabilitiesToRemove.Count);
                    foreach (var item in CapabilitiesToRemove)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryModifyItemCapabilities:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ModifyCapabilitiesRequestId)} = {ModifyCapabilitiesRequestId}\n" +
                   $"  {nameof(ItemToChangeId)} = {ItemToChangeId}\n" +
                   $"  {nameof(CapabilitiesToAdd)} = {String.Join(',', CapabilitiesToAdd)}\n" +
                   $"  {nameof(CapabilitiesToRemove)} = {String.Join(',', CapabilitiesToRemove)}\n";
        }
    }
}
