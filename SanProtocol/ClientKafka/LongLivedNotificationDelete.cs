namespace SanProtocol.ClientKafka
{
    public class LongLivedNotificationDelete : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.LongLivedNotificationDelete;

        public SanUUID Id { get; set; }

        public LongLivedNotificationDelete(SanUUID id)
        {
            Id = id;
        }

        public LongLivedNotificationDelete(BinaryReader br)
        {
            Id = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Id);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::LongLivedNotificationDelete:\n" +
                   $"  {nameof(Id)} = {Id}\n";
        }
    }

}
