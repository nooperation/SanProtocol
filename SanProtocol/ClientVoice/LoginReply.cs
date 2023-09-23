namespace SanProtocol.ClientVoice
{
    public class LoginReply : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.LoginReply;

        public byte Success { get; set; }
        public string Message { get; set; }

        public LoginReply(byte success, string message)
        {
            Success = success;
            Message = message;
        }

        public LoginReply(BinaryReader br)
        {
            Success = br.ReadByte();
            Message = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Success);
                    bw.WriteSanString(Message);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LoginReply:\n" +
                   $"  {nameof(Success)} = {Success}\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}
