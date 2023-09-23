namespace SanProtocol.ClientRegion
{
    public class ScriptModalDialog : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ScriptModalDialog;

        public ulong EventId { get; set; }
        public string Message { get; set; }
        public string LeftButtonLabel { get; set; }
        public string RightButtonLabel { get; set; }

        public ScriptModalDialog(ulong eventId, string message, string leftButtonLabel, string rightButtonLabel)
        {
            EventId = eventId;
            Message = message;
            LeftButtonLabel = leftButtonLabel;
            RightButtonLabel = rightButtonLabel;
        }

        public ScriptModalDialog(BinaryReader br)
        {
            EventId = br.ReadUInt64();
            Message = br.ReadSanString();
            LeftButtonLabel = br.ReadSanString();
            RightButtonLabel = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(EventId);
                    bw.WriteSanString(Message);
                    bw.WriteSanString(LeftButtonLabel);
                    bw.WriteSanString(RightButtonLabel);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ScriptModalDialog:\n" +
                   $"  {nameof(EventId)} = {EventId}\n" +
                   $"  {nameof(Message)} = {Message}\n" +
                   $"  {nameof(LeftButtonLabel)} = {LeftButtonLabel}\n" +
                   $"  {nameof(RightButtonLabel)} = {RightButtonLabel}\n";
        }
    }

}
