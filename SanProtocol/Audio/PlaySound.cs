using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Audio
{
    public class PlaySound : IPacket
    {
        public uint MessageId => Messages.AudioMessages.PlaySound;

        public SanUUID ResourceId { get; set; }
        public ulong CreatePlayHandleId { get; set; }
        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public float Loudness { get; set; }
        public float Pitch { get; set; }
        public uint PlayOffset { get; set; }
        public byte Flags { get; set; }

        public PlaySound(SanUUID resourceId, ulong createPlayHandleId, ulong frame, ulong componentId, List<float> position, float loudness, float pitch, uint playOffset, byte flags)
        {
            this.ResourceId = resourceId;
            this.CreatePlayHandleId = createPlayHandleId;
            this.Frame = frame;
            this.ComponentId = componentId;
            this.Position = position;
            this.Loudness = loudness;
            this.Pitch = pitch;
            this.PlayOffset = playOffset;
            this.Flags = flags;
        }

        public PlaySound(BinaryReader br)
        {
            ResourceId = br.ReadSanUUID();
            CreatePlayHandleId = br.ReadUInt64();
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Position.Add(item);
            }
            Loudness = br.ReadSingle();
            Pitch = br.ReadSingle();
            PlayOffset = br.ReadUInt32();
            Flags = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ResourceId);
                    bw.Write(CreatePlayHandleId);
                    bw.Write(Frame);
                    bw.Write(ComponentId);
                    foreach (var item in Position)
                    {
                        bw.Write(item);
                    }
                    bw.Write(Loudness);
                    bw.Write(Pitch);
                    bw.Write(PlayOffset);
                    bw.Write(Flags);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::PlaySound:\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(CreatePlayHandleId)} = {CreatePlayHandleId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(Loudness)} = {Loudness}\n" +
                   $"  {nameof(Pitch)} = {Pitch}\n" +
                   $"  {nameof(PlayOffset)} = {PlayOffset}\n" +
                   $"  {nameof(Flags)} = {Flags}\n";
        }
    }
}
