﻿namespace SanProtocol.AnimationComponent
{
    public class Int8Variable : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.Int8Variable;

        public ushort InternalId { get; set; }
        public byte Value { get; set; }

        public Int8Variable(ushort internalId, byte value)
        {
            InternalId = internalId;
            Value = value;
        }

        public Int8Variable(BinaryReader br)
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
            return $"AnimationComponent::Int8Variable:\n" +
                   $"  {nameof(InternalId)} = {InternalId}\n" +
                   $"  {nameof(Value)} = {Value}\n";
        }
    }

}
