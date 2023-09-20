using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class Login : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.Login;

        public SanUUID AccountId { get; set; }
        public SanUUID PersonaId { get; set; }
        public uint Secret { get; set; }
        public ulong InventoryOffset { get; set; }

        public Login(SanUUID accountId, SanUUID personaId, uint secret, ulong inventoryOffset)
        {
            AccountId = accountId;
            PersonaId = personaId;
            Secret = secret;
            InventoryOffset = inventoryOffset;
        }

        public Login(BinaryReader br)
        {
            AccountId = br.ReadSanUUID();
            PersonaId = br.ReadSanUUID();
            Secret = br.ReadUInt32();
            InventoryOffset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AccountId);
                    bw.Write(PersonaId);
                    bw.Write(Secret);
                    bw.Write(InventoryOffset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::Login:\n" +
                   $"  {nameof(AccountId)} = {AccountId}\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n" +
                   $"  {nameof(Secret)} = {Secret}\n" +
                   $"  {nameof(InventoryOffset)} = {InventoryOffset}\n";
        }
    }

}
