using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class VectorVariable : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.VectorVariable;

        public ushort InternalId { get; set; }
        public List<float> Value { get; set; } = new List<float>();

        public VectorVariable(ushort internalId, List<float> value)
        {
            this.InternalId = internalId;
            this.Value = value;
        }

        public VectorVariable(BinaryReader br)
        {
            InternalId = br.ReadUInt16();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Value.Add(item);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(InternalId);
                    foreach (var item in Value)
                    {
                        bw.Write(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::VectorVariable:\n" +
                   $"  {nameof(InternalId)} = {InternalId}\n" +
                   $"  {nameof(Value)} = <{String.Join(',', Value)}>\n";
        }
    }
}
