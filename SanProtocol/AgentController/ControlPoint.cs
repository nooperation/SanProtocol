using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class ControlPoint : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.ControlPoint;

        public List<float> Position { get; set; } = new List<float>();
        public Quaternion Orientation { get; set; } = new Quaternion();
        public bool Enabled { get; set; }
        public byte ControlPointType { get; set; }

        public ControlPoint(List<float> position, Quaternion orientation, bool enabled, byte controlPointType)
        {
            this.Position = position;
            this.Orientation = orientation;
            this.Enabled = enabled;
            this.ControlPointType = controlPointType;
        }

        public ControlPoint(BinaryReader br)
        {
            var bitReader = new BitReader(br, 3 * 16 + (3 * 12 + 4) + 1 + 4);
            Position = bitReader.ReadFloats(3, 16, 3.0f);
            Orientation = bitReader.ReadQuaternion(3, 12);
            Enabled = bitReader.ReadUnsigned(1) != 0;
            ControlPointType = (byte)bitReader.ReadUnsigned(4);
        }

        public ControlPoint(BitReader bitReader)
        {
            // 93 bits
            Position = bitReader.ReadFloats(3, 16, 3.0f);
            Orientation = bitReader.ReadQuaternion(3, 12);
            Enabled = bitReader.ReadUnsigned(1) != 0;
            ControlPointType = (byte)bitReader.ReadUnsigned(4);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(Position, 16, 3.0f);
                    bitWriter.WriteQuaternion(Orientation, 12);
                    bitWriter.WriteUnsigned(Enabled ? 1u : 0u, 1);
                    bitWriter.WriteUnsigned(ControlPointType, 4);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public void WriteBits(BitWriter bitWriter)
        {
            // 93 bits
            bitWriter.WriteFloats(Position, 16, 3.0f);
            bitWriter.WriteQuaternion(Orientation, 12);
            bitWriter.WriteUnsigned(Enabled ? 1u : 0u, 1);
            bitWriter.WriteUnsigned(ControlPointType, 4);
        }

        public override string ToString()
        {
            return $"AgentController::ControlPoint:\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(Orientation)} = {Orientation}\n" +
                   $"  {nameof(Enabled)} = {Enabled}\n" +
                   $"  {nameof(ControlPointType)} = {ControlPointType}\n";
        }
    }
}
