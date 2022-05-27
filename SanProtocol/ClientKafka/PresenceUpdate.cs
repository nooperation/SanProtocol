using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class PresenceUpdate : IPacket
    {
        public uint MessageId => Messages.ClientKafka.PresenceUpdate;

        public SanUUID PersonaId { get; set; }
        public byte Present { get; set; }
        public SanUUID SessionId { get; set; }
        public string State { get; set; }
        public string SansarUri { get; set; }

        public PresenceUpdate(SanUUID personaId, byte present, SanUUID sessionId, string state, string sansarUri)
        {
            PersonaId = personaId;
            Present = present;
            SessionId = sessionId;
            State = state;
            SansarUri = sansarUri;
        }

        public PresenceUpdate(BinaryReader br)
        {
            PersonaId = br.ReadSanUUID();
            Present = br.ReadByte();
            SessionId = br.ReadSanUUID();
            State = br.ReadSanString();
            SansarUri = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(PersonaId);
                    bw.Write(Present);
                    bw.Write(SessionId);
                    bw.WriteSanString(State);
                    bw.WriteSanString(SansarUri);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::PresenceUpdate:\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n" +
                   $"  {nameof(Present)} = {Present}\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(State)} = {State}\n" +
                   $"  {nameof(SansarUri)} = {SansarUri}\n";
        }
    }
}
