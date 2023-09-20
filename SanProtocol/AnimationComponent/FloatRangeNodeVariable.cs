using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class FloatRangeNodeVariable : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.FloatRangeNodeVariable;

        public ushort NodeId { get; set; }
        public float StartValue { get; set; }
        public float EndValue { get; set; }

        public FloatRangeNodeVariable(ushort nodeId, float startValue, float endValue)
        {
            this.NodeId = nodeId;
            this.StartValue = startValue;
            this.EndValue = endValue;
        }

        public FloatRangeNodeVariable(BinaryReader br)
        {
            NodeId = br.ReadUInt16();
            StartValue = br.ReadSingle();
            EndValue = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(NodeId);
                    bw.Write(StartValue);
                    bw.Write(EndValue);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::FloatRangeNodeVariable:\n" +
                   $"  {nameof(NodeId)} = {NodeId}\n" +
                   $"  {nameof(StartValue)} = {StartValue}\n" +
                   $"  {nameof(EndValue)} = {EndValue}\n";
        }
    }

}
