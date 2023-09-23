namespace SanProtocol.WorldState
{
    public class RigidBodyComponentInitialState : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.RigidBodyComponentInitialState;

        public uint RelativeComponentId { get; set; }
        public List<float> LinearVelocity { get; set; } = new List<float>();
        public List<float> AngularVelocity { get; set; } = new List<float>();

        public RigidBodyComponentInitialState(uint relativeComponentId, List<float> linearVelocity, List<float> angularVelocity)
        {
            RelativeComponentId = relativeComponentId;
            LinearVelocity = linearVelocity;
            AngularVelocity = angularVelocity;
        }

        public RigidBodyComponentInitialState(BinaryReader br)
        {
            RelativeComponentId = br.ReadUInt32();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                LinearVelocity.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                AngularVelocity.Add(item);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(RelativeComponentId);
                    foreach (var item in LinearVelocity)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in AngularVelocity)
                    {
                        bw.Write(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::RigidBodyComponentInitialState:\n" +
                   $"  {nameof(RelativeComponentId)} = {RelativeComponentId}\n" +
                   $"  {nameof(LinearVelocity)} = <{string.Join(',', LinearVelocity)}>\n" +
                   $"  {nameof(AngularVelocity)} = <{string.Join(',', AngularVelocity)}>\n";
        }
    }
}
