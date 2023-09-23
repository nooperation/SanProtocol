namespace SanProtocol.ClientKafka
{
    public class PrivateChatLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.PrivateChatLoaded;

        public ulong Offset { get; set; }

        public PrivateChatLoaded(ulong offset)
        {
            Offset = offset;
        }

        public PrivateChatLoaded(BinaryReader br)
        {
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::PrivateChatLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
