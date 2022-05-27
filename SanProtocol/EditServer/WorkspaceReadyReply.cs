using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class WorkspaceReadyReply : IPacket
    {
        public uint MessageId => Messages.EditServer.WorkspaceReadyReply;

        public uint WorkspaceId { get; set; }

        public WorkspaceReadyReply(uint workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }

        public WorkspaceReadyReply(BinaryReader br)
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
            return $"EditServer::WorkspaceReadyReply:\n" +
                   $"  {nameof(WorkspaceId)} = {WorkspaceId}\n";
        }
    }
}
