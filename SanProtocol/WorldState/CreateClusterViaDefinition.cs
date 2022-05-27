using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.WorldState
{
    public class CreateClusterViaDefinition : IPacket
    {
        public uint MessageId => Messages.WorldState.CreateClusterViaDefinition;

        public uint ClusterId { get; set; }
        public uint StartingObjectId { get; set; }
        public SanUUID ResourceId { get; set; }
        public List<float> SpawnPosition { get; set; } = new List<float>();
        public List<float> SpawnRotation { get; set; } = new List<float>();

        public CreateClusterViaDefinition(uint clusterId, uint startingObjectId, SanUUID resourceId, List<float> spawnPosition, List<float> spawnRotation)
        {
            this.ClusterId = clusterId;
            this.StartingObjectId = startingObjectId;
            this.ResourceId = resourceId;
            this.SpawnPosition = spawnPosition;
            this.SpawnRotation = spawnRotation;
        }

        public CreateClusterViaDefinition(BinaryReader br)
        {
            ClusterId = br.ReadUInt32();
            StartingObjectId = br.ReadUInt32();
            ResourceId = br.ReadSanUUID();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                SpawnPosition.Add(item);
            }
            for (var i = 0; i < 4; ++i)
            {
                var item = br.ReadSingle();
                SpawnRotation.Add(item);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ClusterId);
                    bw.Write(StartingObjectId);
                    bw.Write(ResourceId);
                    foreach (var item in SpawnPosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in SpawnRotation)
                    {
                        bw.Write(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::CreateClusterViaDefinition:\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n" +
                   $"  {nameof(StartingObjectId)} = {StartingObjectId}\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(SpawnPosition)} = <{String.Join(',', SpawnPosition)}>\n" +
                   $"  {nameof(SpawnRotation)} = <{String.Join(',', SpawnRotation)}>\n";
        }
    }
}
