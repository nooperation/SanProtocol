namespace SanProtocol.ClientKafka
{
    public class SubscribeScriptRegionConsole : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.SubscribeScriptRegionConsole;

        public string InstanceId { get; set; }
        public ulong Offset { get; set; }

        public SubscribeScriptRegionConsole(string instanceId, ulong offset)
        {
            InstanceId = instanceId;
            Offset = offset;
        }

        public SubscribeScriptRegionConsole(BinaryReader br)
        {
            InstanceId = br.ReadSanString();
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(InstanceId);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::SubscribeScriptRegionConsole:\n" +
                   $"  {nameof(InstanceId)} = {InstanceId}\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
