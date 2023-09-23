namespace SanProtocol.RegionRegion
{
    public class AgentControllerMapping : IPacket
    {
        public uint MessageId => Messages.RegionRegionMessages.AgentControllerMapping;

        public uint AgentControllerId { get; set; }
        public ulong AnimationComponentId { get; set; }
        public uint ClusterId { get; set; }

        public AgentControllerMapping(uint agentControllerId, ulong animationComponentId, uint clusterId)
        {
            AgentControllerId = agentControllerId;
            AnimationComponentId = animationComponentId;
            ClusterId = clusterId;
        }

        public AgentControllerMapping(BinaryReader br)
        {
            AgentControllerId = br.ReadUInt32();
            AnimationComponentId = br.ReadUInt64();
            ClusterId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AgentControllerId);
                    bw.Write(AnimationComponentId);
                    bw.Write(ClusterId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"RegionRegion::AgentControllerMapping:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(AnimationComponentId)} = {AnimationComponentId}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n";
        }
    }
}
