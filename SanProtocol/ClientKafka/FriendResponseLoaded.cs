namespace SanProtocol.ClientKafka
{
    public class FriendResponseLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.FriendResponseLoaded;

        public ulong Offset { get; set; }

        public FriendResponseLoaded(ulong offset)
        {
            Offset = offset;
        }

        public FriendResponseLoaded(BinaryReader br)
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
            return $"ClientKafka::FriendResponseLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
