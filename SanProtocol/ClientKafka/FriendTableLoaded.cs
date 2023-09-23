namespace SanProtocol.ClientKafka
{
    public class FriendTableLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.FriendTableLoaded;

        public ulong Offset { get; set; }

        public FriendTableLoaded(ulong offset)
        {
            Offset = offset;
        }

        public FriendTableLoaded(BinaryReader br)
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
            return $"ClientKafka::FriendTableLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
