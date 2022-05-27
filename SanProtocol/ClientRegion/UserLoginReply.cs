using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class UserLoginReply : IPacket
    {
        public uint MessageId => Messages.ClientRegion.UserLoginReply;

        public byte Success { get; set; }
        public uint SessionId { get; set; }
        public string RegionServerVersion { get; set; }
        public List<string> Privileges { get; set; } = new List<string>();

        public UserLoginReply(byte success, uint sessionId, string regionServerVersion, List<string> privileges)
        {
            this.Success = success;
            this.SessionId = sessionId;
            this.RegionServerVersion = regionServerVersion;
            this.Privileges = privileges;
        }

        public UserLoginReply(BinaryReader br)
        {
            Success = br.ReadByte();
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
                   $"  {nameof(Privileges)} = {String.Join(',', Privileges)}\n";
        }
    }
}
