namespace SanProtocol.EditServer
{
    public class WorkspaceReadyReply : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.WorkspaceReadyReply;

        public uint WorkspaceId { get; set; }

        public WorkspaceReadyReply(uint workspaceId)
        {
            WorkspaceId = workspaceId;
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
