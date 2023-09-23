namespace SanProtocol.AnimationComponent
{
    public class PlayAnimation : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.PlayAnimation;

        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public SanUUID ResourceId { get; set; }
        public float PlaybackSpeed { get; set; }
        public byte SkeletonType { get; set; }
        public byte AnimationType { get; set; }
        public byte PlaybackMode { get; set; }

        public PlayAnimation(ulong frame, ulong componentId, SanUUID resourceId, float playbackSpeed, byte skeletonType, byte animationType, byte playbackMode)
        {
            Frame = frame;
            ComponentId = componentId;
            ResourceId = resourceId;
            PlaybackSpeed = playbackSpeed;
            SkeletonType = skeletonType;
            AnimationType = animationType;
            PlaybackMode = playbackMode;
        }

        public PlayAnimation(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            ResourceId = br.ReadSanUUID();

            var bitReader = new BitReader(br, 16 + 2 + 3 + 3);
            PlaybackSpeed = bitReader.ReadFloat(16, 10.0f);
            SkeletonType = (byte)bitReader.ReadUnsigned(2);
            AnimationType = (byte)bitReader.ReadUnsigned(3);
            PlaybackMode = (byte)bitReader.ReadUnsigned(3);
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
                    bw.Write(ResourceId);

                    var bitWriter = new BitWriter();

                    bitWriter.WriteFloat(PlaybackSpeed, 16, 10.0f);
                    bitWriter.WriteUnsigned(SkeletonType, 2);
                    bitWriter.WriteUnsigned(AnimationType, 3);
                    bitWriter.WriteUnsigned(PlaybackMode, 3);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::PlayAnimation:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(PlaybackSpeed)} = {PlaybackSpeed}\n" +
                   $"  {nameof(SkeletonType)} = {SkeletonType}\n" +
                   $"  {nameof(AnimationType)} = {AnimationType}\n" +
                   $"  {nameof(PlaybackMode)} = {PlaybackMode}\n";
        }
    }
}
