using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.GameWorld
{
    public class RiggedMeshScaleChanged : IPacket
    {
        public uint MessageId => Messages.GameWorld.RiggedMeshScaleChanged;

        public ulong Componentid { get; set; }
        public ulong Frame { get; set; }
        public float Scale { get; set; }

        public RiggedMeshScaleChanged(ulong componentid, ulong frame, float scale)
        {
            this.Componentid = componentid;
            this.Frame = frame;
            this.Scale = scale;
        }

        public RiggedMeshScaleChanged(BinaryReader br)
        {
            Componentid = br.ReadUInt64();
            Frame = br.ReadUInt64();
            Scale = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Componentid);
                    bw.Write(Frame);
                    bw.Write(Scale);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::RiggedMeshScaleChanged:\n" +
                   $"  {nameof(Componentid)} = {Componentid}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Scale)} = {Scale}\n";
        }
    }
}
