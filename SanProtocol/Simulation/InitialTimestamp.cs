namespace SanProtocol.Simulation
{
    public class InitialTimestamp : IPacket
    {
        public uint MessageId => Messages.SimulationMessages.InitialTimestamp;

        public ulong Nanoseconds { get; set; }
        public ulong Frame { get; set; }

        public InitialTimestamp(ulong nanoseconds, ulong frame)
        {
            Nanoseconds = nanoseconds;
            Frame = frame;
        }

        public InitialTimestamp(BinaryReader br)
        {
            Nanoseconds = br.ReadUInt64();
            Frame = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Nanoseconds);
                    bw.Write(Frame);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::InitialTimestamp:\n" +
                   $"  {nameof(Nanoseconds)} = {Nanoseconds}\n" +
                   $"  {nameof(Frame)} = {Frame}\n";
        }
    }
}
