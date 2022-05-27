using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class InventoryChangeItemState : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryChangeItemState;

        public string Authorization { get; set; }
        public SanUUID ChangeStateRequestId { get; set; }
        public SanUUID ItemToChangeId { get; set; }
        public byte NewState { get; set; }

        public InventoryChangeItemState(string authorization, SanUUID changeStateRequestId, SanUUID itemToChangeId, byte newState)
        {
            this.Authorization = authorization;
            this.ChangeStateRequestId = changeStateRequestId;
            this.ItemToChangeId = itemToChangeId;
            this.NewState = newState;
        }

        public InventoryChangeItemState(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ChangeStateRequestId = br.ReadSanUUID();
            ItemToChangeId = br.ReadSanUUID();
            NewState = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(ChangeStateRequestId);
                    bw.Write(ItemToChangeId);
                    bw.Write(NewState);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryChangeItemState:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ChangeStateRequestId)} = {ChangeStateRequestId}\n" +
                   $"  {nameof(ItemToChangeId)} = {ItemToChangeId}\n" +
                   $"  {nameof(NewState)} = {NewState}\n";
        }
    }
}
