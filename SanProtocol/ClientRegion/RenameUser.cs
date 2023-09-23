namespace SanProtocol.ClientRegion
{
    public class RenameUser : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.RenameUser;

        public uint SessionId { get; set; }
        public string UserName { get; set; }

        public RenameUser(uint sessionId, string userName)
        {
            SessionId = sessionId;
            UserName = userName;
        }

        public RenameUser(BinaryReader br)
        {
            SessionId = br.ReadUInt32();
            UserName = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(SessionId);
                    bw.WriteSanString(UserName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::RenameUser:\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(UserName)} = {UserName}\n";
        }
    }

}
