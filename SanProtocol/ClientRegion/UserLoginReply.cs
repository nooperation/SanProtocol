namespace SanProtocol.ClientRegion
{
    public class UserLoginReply : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.UserLoginReply;

        public bool Success { get; set; }
        public uint SessionId { get; set; }
        public string RegionServerVersion { get; set; }
        public List<string> Privileges { get; set; } = new List<string>();

        public UserLoginReply(bool success, uint sessionId, string regionServerVersion, List<string> privileges)
        {
            Success = success;
            SessionId = sessionId;
            RegionServerVersion = regionServerVersion;
            Privileges = privileges;
        }

        public UserLoginReply(BinaryReader br)
        {
            Success = br.ReadByte() != 0;
            SessionId = br.ReadUInt32();
            RegionServerVersion = br.ReadSanString();
            var numPrivileges = br.ReadUInt32();
            for (var i = 0; i < numPrivileges; ++i)
            {
                var str = br.ReadSanString();
                Privileges.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Success);
                    bw.Write(SessionId);
                    bw.WriteSanString(RegionServerVersion);
                    bw.Write(Privileges.Count);
                    foreach (var item in Privileges)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UserLoginReply:\n" +
                   $"  {nameof(Success)} = {Success}\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(RegionServerVersion)} = {RegionServerVersion}\n" +
                   $"  {nameof(Privileges)} = {string.Join(',', Privileges)}\n";
        }
    }
}
