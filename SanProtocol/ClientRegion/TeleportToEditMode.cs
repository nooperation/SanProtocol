using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class TeleportToEditMode : IPacket
    {
        public uint MessageId => Messages.ClientRegion.TeleportToEditMode;

        public string ReturnSpawnPointName { get; set; }
        public byte WorkspaceEditView { get; set; }

        public TeleportToEditMode(string returnSpawnPointName, byte workspaceEditView)
        {
            ReturnSpawnPointName = returnSpawnPointName;
            WorkspaceEditView = workspaceEditView;
        }

        public TeleportToEditMode(BinaryReader br)
        {
            ReturnSpawnPointName = br.ReadSanString();
            WorkspaceEditView = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(ReturnSpawnPointName);
                    bw.Write(WorkspaceEditView);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::TeleportToEditMode:\n" +
                   $"  {nameof(ReturnSpawnPointName)} = {ReturnSpawnPointName}\n" +
                   $"  {nameof(WorkspaceEditView)} = {WorkspaceEditView}\n";
        }
    }

}
