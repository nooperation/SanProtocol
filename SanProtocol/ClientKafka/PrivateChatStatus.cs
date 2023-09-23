namespace SanProtocol.ClientKafka
{
    public class PrivateChatStatus : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.PrivateChatStatus;

        public ulong Offset { get; set; }
        public uint Status { get; set; }

        public PrivateChatStatus(ulong offset, uint status)
        {
            Offset = offset;
            Status = status;
        }

        public PrivateChatStatus(BinaryReader br)
        {
            Offset = br.ReadUInt64();
            Status = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Offset);
                    bw.Write(Status);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::PrivateChatStatus:\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(Status)} = {Status}\n";
        }
    }

}
