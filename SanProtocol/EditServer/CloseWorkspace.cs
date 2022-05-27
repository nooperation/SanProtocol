using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class CloseWorkspace : IPacket
    {
        public uint MessageId => Messages.EditServer.CloseWorkspace;

        public uint WorkspaceId { get; set; }

        public CloseWorkspace(uint workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }

        public CloseWorkspace(BinaryReader br)
        {
            WorkspaceId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(WorkspaceId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::CloseWorkspace:\n" +
                   $"  {nameof(WorkspaceId)} = {WorkspaceId}\n";
        }
    }
}
