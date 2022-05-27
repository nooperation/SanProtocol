using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class AddUser : IPacket
    {
        public uint MessageId => Messages.EditServer.AddUser;

        public uint SessionId { get; set; }
        public string UserName { get; set; }
        public SanUUID PersonaId { get; set; }

        public AddUser(uint sessionId, string userName, SanUUID personaId)
        {
            this.SessionId = sessionId;
            this.UserName = userName;
            this.PersonaId = personaId;
        }

        public AddUser(BinaryReader br)
        {
            SessionId = br.ReadUInt32();
            UserName = br.ReadSanString();
            PersonaId = br.ReadSanUUID();
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
                    bw.Write(PersonaId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::AddUser:\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(UserName)} = {UserName}\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n";
        }
    }
}
