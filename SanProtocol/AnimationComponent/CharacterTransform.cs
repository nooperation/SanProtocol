namespace SanProtocol.AnimationComponent
{
    public class CharacterTransform : IPacket
    {
        public virtual uint MessageId => Messages.AnimationComponentMessages.CharacterTransform;

        public ulong ComponentId { get; set; }
        public ulong ServerFrame { get; set; }
        public ulong GroundComponentId { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public Quaternion OrientationQuat { get; set; } = new Quaternion();

        public CharacterTransform(ulong componentId, ulong serverFrame, ulong groundComponentId, List<float> position, Quaternion orientationQuat)
        {
            ComponentId = componentId;
            ServerFrame = serverFrame;
            GroundComponentId = groundComponentId;
            Position = position;
            OrientationQuat = orientationQuat;
        }

        public CharacterTransform(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
            ServerFrame = br.ReadUInt64();
            GroundComponentId = br.ReadUInt64();

            var bitReader = new BitReader(br, (3 * 24) + (3 * 12) + 4);
            Position = bitReader.ReadFloats(3, 24, 2048.0f);
            OrientationQuat = bitReader.ReadQuaternion(3, 12);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ComponentId);
                    bw.Write(ServerFrame);
                    bw.Write(GroundComponentId);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(Position, 24, 2048.0f);
                    bitWriter.WriteQuaternion(OrientationQuat, 12);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::CharacterTransform:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(ServerFrame)} = {ServerFrame}\n" +
                   $"  {nameof(GroundComponentId)} = {GroundComponentId}\n" +
                   $"  {nameof(Position)} = <{string.Join(',', Position)}>\n" +
                   $"  {nameof(OrientationQuat)} = {OrientationQuat}\n";
        }
    }

}
