namespace SanProtocol.Simulation
{
    public class RigidBodyDestroyed : IPacket
    {
        public uint MessageId => Messages.SimulationMessages.RigidBodyDestroyed;

        public ulong ComponentId { get; set; }

        public RigidBodyDestroyed(ulong componentId)
        {
            ComponentId = componentId;
        }

        public RigidBodyDestroyed(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ComponentId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::RigidBodyDestroyed:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n";
        }
    }
}
