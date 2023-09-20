using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class InventoryChangeItemName : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.InventoryChangeItemName;

        public string Authorization { get; set; }
        public SanUUID ChangeNameRequestId { get; set; }
        public SanUUID ItemToChangeId { get; set; }
        public string NewName { get; set; }

        public InventoryChangeItemName(string authorization, SanUUID changeNameRequestId, SanUUID itemToChangeId, string newName)
        {
            this.Authorization = authorization;
            this.ChangeNameRequestId = changeNameRequestId;
            this.ItemToChangeId = itemToChangeId;
            this.NewName = newName;
        }

        public InventoryChangeItemName(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ChangeNameRequestId = br.ReadSanUUID();
            ItemToChangeId = br.ReadSanUUID();
            NewName = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(ChangeNameRequestId);
                    bw.Write(ItemToChangeId);
                    bw.WriteSanString(NewName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryChangeItemName:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ChangeNameRequestId)} = {ChangeNameRequestId}\n" +
                   $"  {nameof(ItemToChangeId)} = {ItemToChangeId}\n" +
                   $"  {nameof(NewName)} = {NewName}\n";
        }
    }
}
