using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class AddUser : IPacket
    {
        public uint MessageId => Messages.ClientRegion.AddUser;

        public uint SessionId { get; set; }
        public string UserName { get; set; }
        public string Handle { get; set; }
        public string AvatarType { get; set; }
        public SanUUID PersonaId { get; set; }

        public AddUser(uint sessionId, string userName, string handle, string avatarType, SanUUID personaId)
        {
            SessionId = sessionId;
            UserName = userName;
            Handle = handle;
            AvatarType = avatarType;
            PersonaId = personaId;
        }

        public AddUser(BinaryReader br)
        {
            SessionId = br.ReadUInt32();
            UserName = br.ReadSanString();
            Handle = br.ReadSanString();
            AvatarType = br.ReadSanString();
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
                    bw.WriteSanString(Handle);
                    bw.WriteSanString(AvatarType);
                    bw.Write(PersonaId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::AddUser:\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(UserName)} = {UserName}\n" +
                   $"  {nameof(Handle)} = {Handle}\n" +
                   $"  {nameof(AvatarType)} = {AvatarType}\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n";
        }
    }

}
