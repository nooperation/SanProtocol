﻿namespace SanProtocol.AnimationComponent
{
    public class FloatVariable : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.FloatVariable;

        public ushort InternalId { get; set; }
        public float Value { get; set; }

        public FloatVariable(ushort internalId, float value)
        {
            InternalId = internalId;
            Value = value;
        }

        public FloatVariable(BinaryReader br)
        {
            InternalId = br.ReadUInt16();
            Value = br.ReadSingle();
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
            return $"AnimationComponent::FloatVariable:\n" +
                   $"  {nameof(InternalId)} = {InternalId}\n" +
                   $"  {nameof(Value)} = {Value}\n";
        }
    }

}
