namespace SanProtocol.ClientKafka
{
    public class InventoryItemCapabilities : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.InventoryItemCapabilities;

        public List<string> Capabilities { get; set; } = new List<string>();

        public InventoryItemCapabilities(List<string> capabilities)
        {
            Capabilities = capabilities;
        }

        public InventoryItemCapabilities(BinaryReader br)
        {
            var numCapabilities = br.ReadUInt32();
            for (var i = 0; i < numCapabilities; ++i)
            {
                var str = br.ReadSanString();
                Capabilities.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Capabilities.Count);
                    foreach (var item in Capabilities)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::InventoryItemCapabilities:\n" +
                   $"  {nameof(Capabilities)} = {string.Join(',', Capabilities)}\n";
        }
    }
}
