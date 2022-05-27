using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.GameWorld
{
    public class StaticMeshFlagsChanged : IPacket
    {
        public uint MessageId => Messages.GameWorld.StaticMeshFlagsChanged;

        public ulong Componentid { get; set; }
        public ulong Frame { get; set; }
        public byte Flags { get; set; }

        public StaticMeshFlagsChanged(ulong componentid, ulong frame, byte flags)
        {
            this.Componentid = componentid;
            this.Frame = frame;
            this.Flags = flags;
        }

        public StaticMeshFlagsChanged(BinaryReader br)
        {
            Componentid = br.ReadUInt64();
            Frame = br.ReadUInt64();
            Flags = br.ReadByte();
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
                    bw.Write(Flags);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::StaticMeshFlagsChanged:\n" +
                   $"  {nameof(Componentid)} = {Componentid}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Flags)} = {Flags}\n";
        }
    }
}
