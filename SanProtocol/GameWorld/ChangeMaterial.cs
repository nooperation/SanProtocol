using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.GameWorld
{
    public class ChangeMaterial : IPacket
    {
        public uint MessageId => Messages.GameWorld.ChangeMaterial;

        public ulong StartFrame { get; set; }
        public ulong ComponentId { get; set; }
        public byte MaterialIndex { get; set; }
        public uint Time { get; set; }
        public byte InterpMode { get; set; } // 4 bits?

        public ChangeMaterial(ulong startFrame, ulong componentId, byte materialIndex, uint time, byte interpMode)
        {
            this.StartFrame = startFrame;
            this.ComponentId = componentId;
            this.MaterialIndex = materialIndex;
            this.Time = time;
            this.InterpMode = interpMode;
        }

        public ChangeMaterial(BinaryReader br)
        {
            StartFrame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            MaterialIndex = br.ReadByte();
            Time = br.ReadUInt32();

            // TODO: Double-Check this... it might need 2x ReadBitsX
            var bitReader = new BitReader(br, 4);
            InterpMode = (byte)bitReader.ReadUnsigned(4);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(StartFrame);
                    bw.Write(ComponentId);
                    bw.Write(MaterialIndex);
                    bw.Write(Time);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteUnsigned(InterpMode, 4);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::ChangeMaterial:\n" +
                   $"  {nameof(StartFrame)} = {StartFrame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(MaterialIndex)} = {MaterialIndex}\n" +
                   $"  {nameof(Time)} = {Time}\n" +
                   $"  {nameof(InterpMode)} = {InterpMode}\n";
        }
    }
}
