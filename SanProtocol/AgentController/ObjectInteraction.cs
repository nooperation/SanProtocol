namespace SanProtocol.AgentController
{
    public class ObjectInteraction : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.ObjectInteraction;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public uint ObjectId { get; set; }
        public List<float> TargetedPosition { get; set; } = new List<float>();
        public List<float> TargetedNormal { get; set; } = new List<float>();
        public List<float> Origin { get; set; } = new List<float>();
        public byte ControlPointType { get; set; } // 4 bits? special nibble?

        public ObjectInteraction(ulong frame, uint agentControllerId, uint objectId, List<float> targetedPosition, List<float> targetedNormal, List<float> origin, byte controlPointType)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            ObjectId = objectId;
            TargetedPosition = targetedPosition;
            TargetedNormal = targetedNormal;
            Origin = origin;
            ControlPointType = controlPointType;
        }

        public ObjectInteraction(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ObjectId = br.ReadUInt32();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                TargetedPosition.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                TargetedNormal.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Origin.Add(item);
            }

            // TODO: Double-Check this... it might need 2x ReadBitsX
            var bitReader = new BitReader(br, 4);
            ControlPointType = (byte)bitReader.ReadUnsigned(4);
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
                    bw.Write(ObjectId);
                    foreach (var item in TargetedPosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in TargetedNormal)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in Origin)
                    {
                        bw.Write(item);
                    }

                    var bitWriter = new BitWriter();
                    bitWriter.WriteUnsigned(ControlPointType, 4);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::ObjectInteraction:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ObjectId)} = {ObjectId}\n" +
                   $"  {nameof(TargetedPosition)} = <{string.Join(',', TargetedPosition)}>\n" +
                   $"  {nameof(TargetedNormal)} = <{string.Join(',', TargetedNormal)}>\n" +
                   $"  {nameof(Origin)} = <{string.Join(',', Origin)}>\n" +
                   $"  {nameof(ControlPointType)} = {ControlPointType}\n";
        }
    }

}
