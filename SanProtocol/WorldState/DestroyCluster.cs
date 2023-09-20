using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.WorldState
{
    public class DestroyCluster : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.DestroyCluster;

        public ulong Frame { get; set; }
        public uint ClusterId { get; set; }

        public DestroyCluster(ulong frame, uint clusterId)
        {
            this.Frame = frame;
            this.ClusterId = clusterId;
        }

        public DestroyCluster(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ClusterId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(ClusterId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::DestroyCluster:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n";
        }
    }
}
