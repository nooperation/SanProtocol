using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.GameWorld
{
    public class RiggedMeshFlagsChange : IPacket
    {
        public uint MessageId => Messages.GameWorldMessages.RiggedMeshFlagsChange;

        public ulong Componentid { get; set; }
        public ulong Frame { get; set; }
        public byte Flags { get; set; }

        public RiggedMeshFlagsChange(ulong componentid, ulong frame, byte flags)
        {
            this.Componentid = componentid;
            this.Frame = frame;
            this.Flags = flags;
        }

        public RiggedMeshFlagsChange(BinaryReader br)
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
            return $"GameWorld::RiggedMeshFlagsChange:\n" +
                   $"  {nameof(Componentid)} = {Componentid}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Flags)} = {Flags}\n";
        }
    }
}
