namespace SanProtocol.ClientVoice
{
    public class VoiceNotification : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.VoiceNotification;

        public string Notification { get; set; }

        public VoiceNotification(string notification)
        {
            Notification = notification;
        }

        public VoiceNotification(BinaryReader br)
        {
            Notification = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Notification);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::VoiceNotification:\n" +
                   $"  {nameof(Notification)} = {Notification}\n";
        }
    }

}
