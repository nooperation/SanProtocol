namespace SanProtocol.WorldState
{
    public class LoadClusterDefinition : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.LoadClusterDefinition;

        public SanUUID ResourceId { get; set; }
        public uint ClusterId { get; set; }

        public LoadClusterDefinition(SanUUID resourceId, uint clusterId)
        {
            ResourceId = resourceId;
            ClusterId = clusterId;
        }

        public LoadClusterDefinition(BinaryReader br)
        {
            ResourceId = br.ReadSanUUID();
            ClusterId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ResourceId);
                    bw.Write(ClusterId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::LoadClusterDefinition:\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n";
        }
    }
}
