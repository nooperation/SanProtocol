namespace SanProtocol.AgentController
{
    public class RequestDeleteLatestSpawn : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.RequestDeleteLatestSpawn;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }

        public RequestDeleteLatestSpawn(ulong frame, uint agentControllerId)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
        }

        public RequestDeleteLatestSpawn(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(AgentControllerId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::RequestDeleteLatestSpawn:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n";
        }
    }
}
