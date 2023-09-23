namespace SanProtocol.ClientRegion
{
    public class ClientVoiceBroadcastStopNotification : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ClientVoiceBroadcastStopNotification;

        public string Message { get; set; }

        public ClientVoiceBroadcastStopNotification(string message)
        {
            Message = message;
        }

        public ClientVoiceBroadcastStopNotification(BinaryReader br)
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
            return $"ClientRegion::ClientVoiceBroadcastStopNotification:\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
