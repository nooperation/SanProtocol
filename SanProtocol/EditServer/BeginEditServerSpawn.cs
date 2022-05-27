using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class BeginEditServerSpawn : IPacket
    {
        public uint MessageId => Messages.EditServer.BeginEditServerSpawn;

        public SanUUID InventoryId { get; set; }
        public SanUUID PersonaId { get; set; }
        public uint Serial { get; set; }
        public string InventoryName { get; set; }

        public BeginEditServerSpawn(SanUUID inventoryId, SanUUID personaId, uint serial, string inventoryName)
        {
            this.InventoryId = inventoryId;
            this.PersonaId = personaId;
            this.Serial = serial;
            this.InventoryName = inventoryName;
        }

        public BeginEditServerSpawn(BinaryReader br)
        {
            InventoryId = br.ReadSanUUID();
            PersonaId = br.ReadSanUUID();
            Serial = br.ReadUInt32();
            InventoryName = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(InventoryId);
                    bw.Write(PersonaId);
                    bw.Write(Serial);
                    bw.WriteSanString(InventoryName);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::BeginEditServerSpawn:\n" +
                   $"  {nameof(InventoryId)} = {InventoryId}\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n" +
                   $"  {nameof(Serial)} = {Serial}\n" +
                   $"  {nameof(InventoryName)} = {InventoryName}\n";
        }
    }
}
