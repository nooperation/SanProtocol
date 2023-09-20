using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class FloatNodeVariable : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.FloatNodeVariable;

        public ushort NodeId { get; set; }
        public float Value { get; set; }

        public FloatNodeVariable(ushort nodeId, float value)
        {
            this.NodeId = nodeId;
            this.Value = value;
        }

        public FloatNodeVariable(BinaryReader br)
        {
            NodeId = br.ReadUInt16();
            Value = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(NodeId);
                    bw.Write(Value);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::FloatNodeVariable:\n" +
                   $"  {nameof(NodeId)} = {NodeId}\n" +
                   $"  {nameof(Value)} = {Value}\n";
        }
    }
}
