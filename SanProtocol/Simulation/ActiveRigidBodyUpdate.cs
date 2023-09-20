using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Simulation
{
    public class ActiveRigidBodyUpdate : IPacket
    {
        public uint MessageId => Messages.SimulationMessages.ActiveRigidBodyUpdate;

        public ulong ComponentId { get; set; }
        public ulong Frame { get; set; }
        public uint OwnerId { get; set; }
        public byte OwnershipWatermark { get; set; }
        public byte Authority { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public Quaternion OrientationQuat { get; set; } = new Quaternion();
        public List<float> LinearVeolcity { get; set; } = new List<float>();
        public List<float> AngularVelocity { get; set; } = new List<float>();

        public ActiveRigidBodyUpdate(ulong componentId, ulong frame, uint ownerId, byte ownershipWatermark, byte authority, List<float> position, Quaternion orientationQuat, List<float> linearVeolcity, List<float> angularVelocity)
        {
            this.ComponentId = componentId;
            this.Frame = frame;
            this.OwnerId = ownerId;
            this.OwnershipWatermark = ownershipWatermark;
            this.Authority = authority;
            this.Position = position;
            this.OrientationQuat = orientationQuat;
            this.LinearVeolcity = linearVeolcity;
            this.AngularVelocity = angularVelocity;
        }

        public ActiveRigidBodyUpdate(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
            Frame = br.ReadUInt64();
            OwnerId = br.ReadUInt32();
            OwnershipWatermark = br.ReadByte();
            Authority = br.ReadByte();

            var bitReader = new BitReader(br, 3 * 26 + (3 * 13 + 4) + 3 * 13 + 3 * 12);
            Position = bitReader.ReadFloats(3, 26, 2048.0f);
            OrientationQuat = bitReader.ReadQuaternion(3, 13);
            LinearVeolcity = bitReader.ReadFloats(3, 13, 256.0f);
            AngularVelocity = bitReader.ReadFloats(3, 12, 256.0f);
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
                    bw.Write(OwnerId);
                    bw.Write(OwnershipWatermark);
                    bw.Write(Authority);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(Position, 26, 2048.0f);
                    bitWriter.WriteQuaternion(OrientationQuat, 13);
                    bitWriter.WriteFloats(LinearVeolcity, 13, 256.0f);
                    bitWriter.WriteFloats(AngularVelocity, 12, 256.0f);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::ActiveRigidBodyUpdate:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(OwnerId)} = {OwnerId}\n" +
                   $"  {nameof(OwnershipWatermark)} = {OwnershipWatermark}\n" +
                   $"  {nameof(Authority)} = {Authority}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(OrientationQuat)} = <{String.Join(',', OrientationQuat.Values)}>\n" +
                   $"  {nameof(LinearVeolcity)} = <{String.Join(',', LinearVeolcity)}>\n" +
                   $"  {nameof(AngularVelocity)} = <{String.Join(',', AngularVelocity)}>\n";
        }
    }
}
