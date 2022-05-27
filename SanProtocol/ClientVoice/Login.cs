using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class Login : IPacket
    {
        public uint MessageId => Messages.ClientVoice.Login;

        public SanUUID Instance { get; set; }
        public uint Secret { get; set; }
        public SanUUID PersonaId { get; set; }
        public byte Slave { get; set; }

        public Login(SanUUID instance, uint secret, SanUUID personaId, byte slave)
        {
            Instance = instance;
            Secret = secret;
            PersonaId = personaId;
            Slave = slave;
        }

        public Login(BinaryReader br)
        {
            Instance = br.ReadSanUUID();
            Secret = br.ReadUInt32();
            PersonaId = br.ReadSanUUID();
            Slave = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Instance);
                    bw.Write(Secret);
                    bw.Write(PersonaId);
                    bw.Write(Slave);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::Login:\n" +
                   $"  {nameof(Instance)} = {Instance}\n" +
                   $"  {nameof(Secret)} = {Secret}\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n" +
                   $"  {nameof(Slave)} = {Slave}\n";
        }
    }

}
