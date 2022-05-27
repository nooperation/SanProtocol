using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.WorldState
{
    public class CreateWorld : IPacket
    {
        public uint MessageId => Messages.WorldState.CreateWorld;

        public SanUUID WorldDefinition { get; set; }
        public uint StartingClusterId { get; set; }
        public uint StartingObjectId { get; set; }

        public CreateWorld(SanUUID worldDefinition, uint startingClusterId, uint startingObjectId)
        {
            this.WorldDefinition = worldDefinition;
            this.StartingClusterId = startingClusterId;
            this.StartingObjectId = startingObjectId;
        }

        public CreateWorld(BinaryReader br)
        {
            WorldDefinition = br.ReadSanUUID();
            StartingClusterId = br.ReadUInt32();
            StartingObjectId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(WorldDefinition);
                    bw.Write(StartingClusterId);
                    bw.Write(StartingObjectId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::CreateWorld:\n" +
                   $"  {nameof(WorldDefinition)} = {WorldDefinition}\n" +
                   $"  {nameof(StartingClusterId)} = {StartingClusterId}\n" +
                   $"  {nameof(StartingObjectId)} = {StartingObjectId}\n";
        }
    }
}
