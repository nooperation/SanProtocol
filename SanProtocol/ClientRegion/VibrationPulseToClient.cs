namespace SanProtocol.ClientRegion
{
    public class VibrationPulseToClient : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.VibrationPulseToClient;

        public uint ControlPointType { get; set; }
        public float Intensity { get; set; }
        public float Duration { get; set; }

        public VibrationPulseToClient(uint controlPointType, float intensity, float duration)
        {
            ControlPointType = controlPointType;
            Intensity = intensity;
            Duration = duration;
        }

        public VibrationPulseToClient(BinaryReader br)
        {
            ControlPointType = br.ReadUInt32();
            Intensity = br.ReadSingle();
            Duration = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ControlPointType);
                    bw.Write(Intensity);
                    bw.Write(Duration);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::VibrationPulseToClient:\n" +
                   $"  {nameof(ControlPointType)} = {ControlPointType}\n" +
                   $"  {nameof(Intensity)} = {Intensity}\n" +
                   $"  {nameof(Duration)} = {Duration}\n";
        }
    }

}
