namespace SanProtocol.WorldState
{
    public class DestroyAgentController : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.DestroyAgentController;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }

        public DestroyAgentController(ulong frame, uint agentControllerId)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
        }

        public DestroyAgentController(BinaryReader br)
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
            return $"WorldState::DestroyAgentController:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n";
        }
    }
}
