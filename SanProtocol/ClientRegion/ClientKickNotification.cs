namespace SanProtocol.ClientRegion
{
    public class ClientKickNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ClientKickNotification;

        public string Message { get; set; }

        public ClientKickNotification(string message)
        {
            Message = message;
        }

        public ClientKickNotification(BinaryReader br)
        {
            Message = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Message);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientKickNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
