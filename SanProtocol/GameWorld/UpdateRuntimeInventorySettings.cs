using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.GameWorld
{
    public class UpdateRuntimeInventorySettings : IPacket
    {
        public uint MessageId => Messages.GameWorld.UpdateRuntimeInventorySettings;

        public byte SpawnSource { get; set; }
        public byte SpawnLifetimePolicy { get; set; }
        public ushort TotalSpawnLimit { get; set; }
        public ushort PerUserSpawnLimit { get; set; }
        public uint SpawnTimeout { get; set; }

        public UpdateRuntimeInventorySettings(byte spawnSource, byte spawnLifetimePolicy, ushort totalSpawnLimit, ushort perUserSpawnLimit, uint spawnTimeout)
        {
            this.SpawnSource = spawnSource;
            this.SpawnLifetimePolicy = spawnLifetimePolicy;
            this.TotalSpawnLimit = totalSpawnLimit;
            this.PerUserSpawnLimit = perUserSpawnLimit;
            this.SpawnTimeout = spawnTimeout;
        }

        public UpdateRuntimeInventorySettings(BinaryReader br)
        {
            SpawnSource = br.ReadByte();
            SpawnLifetimePolicy = br.ReadByte();
            TotalSpawnLimit = br.ReadUInt16();
            PerUserSpawnLimit = br.ReadUInt16();
            SpawnTimeout = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(SpawnSource);
                    bw.Write(SpawnLifetimePolicy);
                    bw.Write(TotalSpawnLimit);
                    bw.Write(PerUserSpawnLimit);
                    bw.Write(SpawnTimeout);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::UpdateRuntimeInventorySettings:\n" +
                   $"  {nameof(SpawnSource)} = {SpawnSource}\n" +
                   $"  {nameof(SpawnLifetimePolicy)} = {SpawnLifetimePolicy}\n" +
                   $"  {nameof(TotalSpawnLimit)} = {TotalSpawnLimit}\n" +
                   $"  {nameof(PerUserSpawnLimit)} = {PerUserSpawnLimit}\n" +
                   $"  {nameof(SpawnTimeout)} = {SpawnTimeout}\n";
        }
    }
}
