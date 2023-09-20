using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class UserLoginReply : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.UserLoginReply;

        public byte Success { get; set; }
        public uint SessionId { get; set; }
        public string EditServerVersion { get; set; }

        public UserLoginReply(byte success, uint sessionId, string editServerVersion)
        {
            this.Success = success;
            this.SessionId = sessionId;
            this.EditServerVersion = editServerVersion;
        }

        public UserLoginReply(BinaryReader br)
        {
            Success = br.ReadByte();
            SessionId = br.ReadUInt32();
            EditServerVersion = br.ReadSanString();
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
                    bw.WriteSanString(EditServerVersion);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::UserLoginReply:\n" +
                   $"  {nameof(Success)} = {Success}\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(EditServerVersion)} = {EditServerVersion}\n";
        }
    }
}
