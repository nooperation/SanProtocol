namespace SanProtocol.EditServer
{
    public class UserLogin : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.UserLogin;

        public string Authorization { get; set; }
        public uint Secret { get; set; }

        public UserLogin(string authorization, uint secret)
        {
            Authorization = authorization;
            Secret = secret;
        }

        public UserLogin(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            Secret = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(Secret);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::UserLogin:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(Secret)} = {Secret}\n";
        }
    }
}
