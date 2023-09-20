using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Simulation
{
    public class RigidBodyPropertyChanged : IPacket
    {
        public uint MessageId => Messages.SimulationMessages.RigidBodyPropertyChanged;

        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public byte[] PropertyData { get; set; }
        public byte PropertyType { get; set; }

        public RigidBodyPropertyChanged(ulong frame, ulong componentId, byte[] propertyData, byte propertyType)
        {
            this.Frame = frame;
            this.ComponentId = componentId;
            this.PropertyData = propertyData;
            this.PropertyType = propertyType;
        }

        public RigidBodyPropertyChanged(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            PropertyData = br.ReadBytes(16);

            // TODO: May require 2x BitReaderX
            var bitReader = new BitReader(br, 5);
            PropertyType = (byte)bitReader.ReadUnsigned(5);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(ComponentId);
                    bw.Write(PropertyData);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteUnsigned(PropertyType, 5);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::RigidBodyPropertyChanged:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(PropertyData)} = {PropertyData}\n" +
                   $"  {nameof(PropertyType)} = {PropertyType}\n";
        }
    }
}
