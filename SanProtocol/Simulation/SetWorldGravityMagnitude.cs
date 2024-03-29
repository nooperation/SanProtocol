﻿namespace SanProtocol.Simulation
{
    public class SetWorldGravityMagnitude : IPacket
    {
        public uint MessageId => Messages.SimulationMessages.SetWorldGravityMagnitude;

        public ulong Frame { get; set; }
        public float Magnitude { get; set; }

        public SetWorldGravityMagnitude(ulong frame, float magnitude)
        {
            Frame = frame;
            Magnitude = magnitude;
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
