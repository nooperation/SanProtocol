namespace SanProtocol.ClientVoice
{
    public class LocalSetRegionBroadcasted : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.LocalSetRegionBroadcasted;

        public byte Broadcasted { get; set; }

        public LocalSetRegionBroadcasted(byte broadcasted)
        {
            Broadcasted = broadcasted;
        }

        public LocalSetRegionBroadcasted(BinaryReader br)
        {
            Broadcasted = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Broadcasted);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalSetRegionBroadcasted:\n" +
                   $"  {nameof(Broadcasted)} = {Broadcasted}\n";
        }
    }

}
