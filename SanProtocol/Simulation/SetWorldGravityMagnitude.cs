using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.Simulation
{
    public class SetWorldGravityMagnitude : IPacket
    {
        public uint MessageId => Messages.Simulation.SetWorldGravityMagnitude;

        public ulong Frame { get; set; }
        public float Magnitude { get; set; }

        public SetWorldGravityMagnitude(ulong frame, float magnitude)
        {
            this.Frame = frame;
            this.Magnitude = magnitude;
        }

        public SetWorldGravityMagnitude(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            Magnitude = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(Magnitude);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::SetWorldGravityMagnitude:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Magnitude)} = {Magnitude}\n";
        }
    }
}
