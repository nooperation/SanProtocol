namespace SanProtocol.ClientKafka
{
    public class ClientMetric : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.ClientMetric;

        public string JsonString { get; set; }

        public ClientMetric(string jsonString)
        {
            JsonString = jsonString;
        }

        public ClientMetric(BinaryReader br)
        {
            JsonString = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(JsonString);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::ClientMetric:\n" +
                   $"  {nameof(JsonString)} = {JsonString}\n";
        }
    }
}
