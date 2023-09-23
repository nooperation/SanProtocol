namespace SanProtocol.ClientVoice
{
    public class VoiceModerationCommandResponse : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.VoiceModerationCommandResponse;

        public string Message { get; set; }
        public byte Success { get; set; }

        public VoiceModerationCommandResponse(string message, byte success)
        {
            Message = message;
            Success = success;
        }

        public VoiceModerationCommandResponse(BinaryReader br)
        {
            Message = br.ReadSanString();
            Success = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Message);
                    bw.Write(Success);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::VoiceModerationCommandResponse:\n" +
                   $"  {nameof(Message)} = {Message}\n" +
                   $"  {nameof(Success)} = {Success}\n";
        }
    }

}
