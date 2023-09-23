namespace SanProtocol.Audio
{
    public class PerformMediaAction : IPacket
    {
        public uint MessageId => Messages.AudioMessages.PerformMediaAction;

        public uint MediaAction { get; set; }
        public byte Rebroadcast { get; set; }

        public PerformMediaAction(uint mediaAction, byte rebroadcast)
        {
            MediaAction = mediaAction;
            Rebroadcast = rebroadcast;
        }

        public PerformMediaAction(BinaryReader br)
        {
            MediaAction = br.ReadUInt32();
            Rebroadcast = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(MediaAction);
                    bw.Write(Rebroadcast);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::PerformMediaAction:\n" +
                   $"  {nameof(MediaAction)} = {MediaAction}\n" +
                   $"  {nameof(Rebroadcast)} = {Rebroadcast}\n";
        }
    }
}
