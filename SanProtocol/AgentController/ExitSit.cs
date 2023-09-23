namespace SanProtocol.AgentController
{
    public class ExitSit : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.ExitSit;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public ulong ComponentId { get; set; }
        public bool SkipAnimation { get; set; }
        public bool SkipExitTeleport { get; set; }

        public ExitSit(ulong frame, uint agentControllerId, ulong componentId, bool skipAnimation, bool skipExitTeleport)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            ComponentId = componentId;
            SkipAnimation = skipAnimation;
            SkipExitTeleport = skipExitTeleport;
        }

        public ExitSit(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ComponentId = br.ReadUInt64();

            var bitReader = new BitReader(br, 2);
            SkipAnimation = bitReader.ReadUnsigned(1) != 0;
            SkipExitTeleport = bitReader.ReadUnsigned(1) != 0;
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(AgentControllerId);
                    bw.Write(ComponentId);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteUnsigned(SkipAnimation ? 1u : 0u, 1);
                    bitWriter.WriteUnsigned(SkipExitTeleport ? 1u : 0u, 1);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::ExitSit:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(SkipAnimation)} = {SkipAnimation}\n" +
                   $"  {nameof(SkipExitTeleport)} = {SkipExitTeleport}\n";
        }
    }

}
