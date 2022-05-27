using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AnimationComponent
{
    public class BoolVariable : IPacket
    {
        public uint MessageId => Messages.AnimationComponent.BoolVariable;

        public ushort InternalId { get; set; }
        public byte Value { get; set; }

        public BoolVariable(ushort internalId, byte value)
        {
            this.InternalId = internalId;
            this.Value = value;
        }

        public BoolVariable(BinaryReader br)
        {
            InternalId = br.ReadUInt16();
            Value = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(InternalId);
                    bw.Write(Value);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::BoolVariable:\n" +
                   $"  {nameof(InternalId)} = {InternalId}\n" +
                   $"  {nameof(Value)} = {Value}\n";
        }
    }
}
