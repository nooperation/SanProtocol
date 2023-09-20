using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class CharacterSetPosition : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.CharacterSetPosition;

        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public ulong GroundComponentId { get; set; }
        public List<float> Position { get; set; } = new List<float>();

        public CharacterSetPosition(ulong frame, ulong componentId, ulong groundComponentId, List<float> position)
        {
            this.Frame = frame;
            this.ComponentId = componentId;
            this.GroundComponentId = groundComponentId;
            this.Position = position;
        }

        public CharacterSetPosition(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            GroundComponentId = br.ReadUInt64();

            var bitReader = new BitReader(br, 3 * 24);
            Position = bitReader.ReadFloats(3, 24, 2048.0f);
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
                    bw.Write(GroundComponentId);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(Position, 24, 2048.0f);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::CharacterSetPosition:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(GroundComponentId)} = {GroundComponentId}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n";
        }
    }
}
