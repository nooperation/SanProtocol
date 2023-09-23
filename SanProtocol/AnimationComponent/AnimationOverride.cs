namespace SanProtocol.AnimationComponent
{
    public class AnimationOverride : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.AnimationOverride;

        public byte SlotIndex { get; set; }

        public AnimationOverride(byte slotIndex)
        {
            SlotIndex = slotIndex;
        }

        public AnimationOverride(BinaryReader br)
        {
            SlotIndex = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(SlotIndex);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::AnimationOverride:\n" +
                   $"  {nameof(SlotIndex)} = {SlotIndex}\n";
        }
    }
}
