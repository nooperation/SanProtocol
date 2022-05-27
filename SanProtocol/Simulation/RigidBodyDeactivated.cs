using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.Simulation
{
    public class RigidBodyDeactivated : IPacket
    {
        public uint MessageId => Messages.Simulation.RigidBodyDeactivated;

        public ulong ComponentId { get; set; }
        public ulong Frame { get; set; }
        public byte OwnershipWatermark { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public Quaternion OrientationQuat { get; set; } = new Quaternion();

        public RigidBodyDeactivated(ulong componentId, ulong frame, byte ownershipWatermark, List<float> position, Quaternion orientationQuat)
        {
            this.ComponentId = componentId;
            this.Frame = frame;
            this.OwnershipWatermark = ownershipWatermark;
            this.Position = position;
            this.OrientationQuat = orientationQuat;
        }

        public RigidBodyDeactivated(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
            Frame = br.ReadUInt64();
            OwnershipWatermark = br.ReadByte();

            var bitReader = new BitReader(br, 3 * 26 + (3 * 13 + 4));
            Position = bitReader.ReadFloats(3, 26, 2048.0f);
            OrientationQuat = bitReader.ReadQuaternion(3, 13);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ComponentId);
                    bw.Write(Frame);
                    bw.Write(OwnershipWatermark);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(Position, 26, 2048.0f);
                    bitWriter.WriteQuaternion(OrientationQuat, 13);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::RigidBodyDeactivated:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(OwnershipWatermark)} = {OwnershipWatermark}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(OrientationQuat)} = {OrientationQuat}\n";
        }
    }
}
