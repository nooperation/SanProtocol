namespace SanProtocol.ClientKafka
{
    public class FriendRequestStatus : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.FriendRequestStatus;

        public ulong Offset { get; set; }
        public uint Status { get; set; }

        public FriendRequestStatus(ulong offset, uint status)
        {
            Offset = offset;
            Status = status;
        }

        public FriendRequestStatus(BinaryReader br)
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
            return $"ClientKafka::FriendRequestStatus:\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(Status)} = {Status}\n";
        }
    }

}
