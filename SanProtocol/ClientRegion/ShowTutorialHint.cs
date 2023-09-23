namespace SanProtocol.ClientRegion
{
    public class ShowTutorialHint : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ShowTutorialHint;

        public uint TutorialHintEnum { get; set; }
        public uint Variant { get; set; }

        public ShowTutorialHint(uint tutorialHintEnum, uint variant)
        {
            TutorialHintEnum = tutorialHintEnum;
            Variant = variant;
        }

        public ShowTutorialHint(BinaryReader br)
        {
            TutorialHintEnum = br.ReadUInt32();
            Variant = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(TutorialHintEnum);
                    bw.Write(Variant);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ShowTutorialHint:\n" +
                   $"  {nameof(TutorialHintEnum)} = {TutorialHintEnum}\n" +
                   $"  {nameof(Variant)} = {Variant}\n";
        }
    }

}
