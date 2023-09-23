namespace SanProtocol.ClientKafka
{
    public class PresenceUpdateFanoutLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.PresenceUpdateFanoutLoaded;

        public ulong Offset { get; set; }

        public PresenceUpdateFanoutLoaded(ulong offset)
        {
            Offset = offset;
        }

        public PresenceUpdateFanoutLoaded(BinaryReader br)
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
            return $"ClientKafka::PresenceUpdateFanoutLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
