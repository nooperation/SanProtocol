﻿namespace SanProtocol.EditServer
{
    public class RemoveUser : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.RemoveUser;

        public uint SessionId { get; set; }

        public RemoveUser(uint sessionId)
        {
            SessionId = sessionId;
        }

        public RemoveUser(BinaryReader br)
        {
            SessionId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(SessionId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::RemoveUser:\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n";
        }
    }
}
