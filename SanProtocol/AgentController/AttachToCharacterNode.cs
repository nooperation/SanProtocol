namespace SanProtocol.AgentController
{
    public class AttachToCharacterNode : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.AttachToCharacterNode;

        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public uint AgentControllerId { get; set; }
        public List<float> AttachmentOffsetPosition { get; set; } = new List<float>();
        public List<float> AttachmentOffsetOrientation { get; set; } = new List<float>();
        public byte NodeType { get; set; }
        public byte OwnershipWatermark { get; set; }
        public byte BroadcastToSelf { get; set; }

        public AttachToCharacterNode(ulong frame, ulong componentId, uint agentControllerId, List<float> attachmentOffsetPosition, List<float> attachmentOffsetOrientation, byte nodeType, byte ownershipWatermark, byte broadcastToSelf)
        {
            Frame = frame;
            ComponentId = componentId;
            AgentControllerId = agentControllerId;
            AttachmentOffsetPosition = attachmentOffsetPosition;
            AttachmentOffsetOrientation = attachmentOffsetOrientation;
            NodeType = nodeType;
            OwnershipWatermark = ownershipWatermark;
            BroadcastToSelf = broadcastToSelf;
        }

        public AttachToCharacterNode(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                AttachmentOffsetPosition.Add(item);
            }
            for (var i = 0; i < 4; ++i)
            {
                var item = br.ReadSingle();
                AttachmentOffsetOrientation.Add(item);
            }
            NodeType = br.ReadByte();
            OwnershipWatermark = br.ReadByte();
            BroadcastToSelf = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(ComponentId);
                    bw.Write(AgentControllerId);
                    foreach (var item in AttachmentOffsetPosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in AttachmentOffsetOrientation)
                    {
                        bw.Write(item);
                    }
                    bw.Write(NodeType);
                    bw.Write(OwnershipWatermark);
                    bw.Write(BroadcastToSelf);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::AttachToCharacterNode:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(AttachmentOffsetPosition)} = <{string.Join(',', AttachmentOffsetPosition)}>\n" +
                   $"  {nameof(AttachmentOffsetOrientation)} = <{string.Join(',', AttachmentOffsetOrientation)}>\n" +
                   $"  {nameof(NodeType)} = {NodeType}\n" +
                   $"  {nameof(OwnershipWatermark)} = {OwnershipWatermark}\n" +
                   $"  {nameof(BroadcastToSelf)} = {BroadcastToSelf}\n";
        }
    }
}
