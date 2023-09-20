using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ClientDynamicReady : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ClientDynamicReady;

        public List<float> Position { get; set; } = new List<float>();
        public List<float> Orientation { get; set; } = new List<float>();
        public SanUUID TargetPersonaId { get; set; }
        public string TargetSpawnPointName { get; set; }
        public byte SpawnStyle { get; set; }
        public byte Ready { get; set; }

        public ClientDynamicReady(List<float> position, List<float> orientation, SanUUID targetPersonaId, string targetSpawnPointName, byte spawnStyle, byte ready)
        {
            this.Position = position;
            this.Orientation = orientation;
            this.TargetPersonaId = targetPersonaId;
            this.TargetSpawnPointName = targetSpawnPointName;
            this.SpawnStyle = spawnStyle;
            this.Ready = ready;
        }

        public ClientDynamicReady(BinaryReader br)
        {
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Position.Add(item);
            }
            for (var i = 0; i < 4; ++i)
            {
                var item = br.ReadSingle();
                Orientation.Add(item);
            }
            TargetPersonaId = br.ReadSanUUID();
            TargetSpawnPointName = br.ReadSanString();
            SpawnStyle = br.ReadByte();
            Ready = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    foreach (var item in Position)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in Orientation)
                    {
                        bw.Write(item);
                    }
                    bw.Write(TargetPersonaId);
                    bw.WriteSanString(TargetSpawnPointName);
                    bw.Write(SpawnStyle);
                    bw.Write(Ready);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientDynamicReady:\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(Orientation)} = <{String.Join(',', Orientation)}>\n" +
                   $"  {nameof(TargetPersonaId)} = {TargetPersonaId}\n" +
                   $"  {nameof(TargetSpawnPointName)} = {TargetSpawnPointName}\n" +
                   $"  {nameof(SpawnStyle)} = {SpawnStyle}\n" +
                   $"  {nameof(Ready)} = {Ready}\n";
        }
    }
}
