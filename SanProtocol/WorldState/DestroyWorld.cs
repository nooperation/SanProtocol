using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.WorldState
{
    public class DestroyWorld : IPacket
    {
        public uint MessageId => Messages.WorldState.DestroyWorld;

        public uint WorldId { get; set; }

        public DestroyWorld(uint worldId)
        {
            this.WorldId = worldId;
        }

        public DestroyWorld(BinaryReader br)
        {
            WorldId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(WorldId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::DestroyWorld:\n" +
                   $"  {nameof(WorldId)} = {WorldId}\n";
        }
    }
}
