namespace SanProtocol.Audio
{
    public class PlayStream : IPacket
    {
        public uint MessageId => Messages.AudioMessages.PlayStream;

        public byte StreamChannel { get; set; }
        public ulong CreatePlayHandleId { get; set; }
        public ulong ComponentId { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public float Loudness { get; set; }
        public float Pitch { get; set; }
        public byte Flags { get; set; }

        public PlayStream(byte streamChannel, ulong createPlayHandleId, ulong componentId, List<float> position, float loudness, float pitch, byte flags)
        {
            StreamChannel = streamChannel;
            CreatePlayHandleId = createPlayHandleId;
            ComponentId = componentId;
            Position = position;
            Loudness = loudness;
            Pitch = pitch;
            Flags = flags;
        }

        public PlayStream(BinaryReader br)
        {
            StreamChannel = br.ReadByte();
            CreatePlayHandleId = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Position.Add(item);
            }
            Loudness = br.ReadSingle();
            Pitch = br.ReadSingle();
            Flags = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(StreamChannel);
                    bw.Write(CreatePlayHandleId);
                    bw.Write(ComponentId);
                    foreach (var item in Position)
                    {
                        bw.Write(item);
                    }
                    bw.Write(Loudness);
                    bw.Write(Pitch);
                    bw.Write(Flags);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::PlayStream:\n" +
                   $"  {nameof(StreamChannel)} = {StreamChannel}\n" +
                   $"  {nameof(CreatePlayHandleId)} = {CreatePlayHandleId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Position)} = <{string.Join(',', Position)}>\n" +
                   $"  {nameof(Loudness)} = {Loudness}\n" +
                   $"  {nameof(Pitch)} = {Pitch}\n" +
                   $"  {nameof(Flags)} = {Flags}\n";
        }
    }

}
