﻿namespace SanProtocol.GameWorld
{
    public class StaticMeshScaleChanged : IPacket
    {
        public uint MessageId => Messages.GameWorldMessages.StaticMeshScaleChanged;

        public ulong Componentid { get; set; }
        public ulong Frame { get; set; }
        public float Scale { get; set; }

        public StaticMeshScaleChanged(ulong componentid, ulong frame, float scale)
        {
            Componentid = componentid;
            Frame = frame;
            Scale = scale;
        }

        public StaticMeshScaleChanged(BinaryReader br)
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
            return $"GameWorld::StaticMeshScaleChanged:\n" +
                   $"  {nameof(Componentid)} = {Componentid}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Scale)} = {Scale}\n";
        }
    }
}
