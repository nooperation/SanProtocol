using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class SaveWorkspaceSelectionToInventory : IPacket
    {
        public uint MessageId => Messages.EditServer.SaveWorkspaceSelectionToInventory;

        public string Authorization { get; set; }
        public string ItemName { get; set; }
        public ulong SelectionId { get; set; }
        public uint TriggeringState { get; set; }
        public uint ParentInstanceId { get; set; }

        public SaveWorkspaceSelectionToInventory(string authorization, string itemName, ulong selectionId, uint triggeringState, uint parentInstanceId)
        {
            this.Authorization = authorization;
            this.ItemName = itemName;
            this.SelectionId = selectionId;
            this.TriggeringState = triggeringState;
            this.ParentInstanceId = parentInstanceId;
        }

        public SaveWorkspaceSelectionToInventory(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            ItemName = br.ReadSanString();
            SelectionId = br.ReadUInt64();
            TriggeringState = br.ReadUInt32();
            ParentInstanceId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.WriteSanString(ItemName);
                    bw.Write(SelectionId);
                    bw.Write(TriggeringState);
                    bw.Write(ParentInstanceId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::SaveWorkspaceSelectionToInventory:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n" +
                   $"  {nameof(SelectionId)} = {SelectionId}\n" +
                   $"  {nameof(TriggeringState)} = {TriggeringState}\n" +
                   $"  {nameof(ParentInstanceId)} = {ParentInstanceId}\n";
        }
    }
}
