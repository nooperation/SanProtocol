using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.WorldState
{
    public class LoadClusterDefinition : IPacket
    {
        public uint MessageId => Messages.WorldState.LoadClusterDefinition;

        public SanUUID ResourceId { get; set; }
        public uint ClusterId { get; set; }

        public LoadClusterDefinition(SanUUID resourceId, uint clusterId)
        {
            this.ResourceId = resourceId;
            this.ClusterId = clusterId;
        }

        public LoadClusterDefinition(BinaryReader br)
        {
            ResourceId = br.ReadSanUUID();
            ClusterId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ResourceId);
                    bw.Write(ClusterId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::LoadClusterDefinition:\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n";
        }
    }
}
