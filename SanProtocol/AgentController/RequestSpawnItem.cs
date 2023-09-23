namespace SanProtocol.AgentController
{
    public class RequestSpawnItem : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.RequestSpawnItem;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public SanUUID ResourceId { get; set; }
        public byte AttachmentNode { get; set; }
        public List<float> SpawnPosition { get; set; } = new List<float>();
        public Quaternion SpawnOrientation { get; set; } = new Quaternion();

        public RequestSpawnItem(ulong frame, uint agentControllerId, SanUUID resourceId, byte attachmentNode, List<float> spawnPosition, Quaternion spawnOrientation)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            ResourceId = resourceId;
            AttachmentNode = attachmentNode;
            SpawnPosition = spawnPosition;
            SpawnOrientation = spawnOrientation;
        }


        public RequestSpawnItem(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ResourceId = br.ReadSanUUID();
            AttachmentNode = br.ReadByte();

            var bitReader = new BitReader(br, (3 * 26) + (3 * 13) + 4);
            SpawnPosition = bitReader.ReadFloats(3, 26, 2048.0f);
            SpawnOrientation = bitReader.ReadQuaternion(3, 13);
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
                    bw.Write(ResourceId);
                    bw.Write(AttachmentNode);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(SpawnPosition, 26, 2048.0f);
                    bitWriter.WriteQuaternion(SpawnOrientation, 13);
                    var bits = bitWriter.GetBytes();
                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::RequestSpawnItem:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(AttachmentNode)} = {AttachmentNode}\n" +
                   $"  {nameof(SpawnPosition)} = <{string.Join(',', SpawnPosition)}>\n" +
                   $"  {nameof(SpawnOrientation)} = {SpawnOrientation}\n";
        }
    }

}
