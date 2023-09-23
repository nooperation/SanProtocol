namespace SanProtocol.AgentController
{
    public class WarpCharacter : IPacket
    {
        public virtual uint MessageId => Messages.AgentControllerMessages.WarpCharacter;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public float Position_x { get; set; }
        public float Position_y { get; set; }
        public float Position_z { get; set; }
        public float Rotation_x { get; set; }
        public float Rotation_y { get; set; }
        public float Rotation_z { get; set; }
        public float Rotation_w { get; set; }

        public WarpCharacter(ulong frame, uint agentControllerId, float position_x, float position_y, float position_z, float rotation_x, float rotation_y, float rotation_z, float rotation_w)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            Position_x = position_x;
            Position_y = position_y;
            Position_z = position_z;
            Rotation_x = rotation_x;
            Rotation_y = rotation_y;
            Rotation_z = rotation_z;
            Rotation_w = rotation_w;
        }

        public WarpCharacter(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            Position_x = br.ReadSingle();
            Position_y = br.ReadSingle();
            Position_z = br.ReadSingle();
            Rotation_x = br.ReadSingle();
            Rotation_y = br.ReadSingle();
            Rotation_z = br.ReadSingle();
            Rotation_w = br.ReadSingle();
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
                    bw.Write(Position_x);
                    bw.Write(Position_y);
                    bw.Write(Position_z);
                    bw.Write(Rotation_x);
                    bw.Write(Rotation_y);
                    bw.Write(Rotation_z);
                    bw.Write(Rotation_w);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::WarpCharacter:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Position_x)} = {Position_x}\n" +
                   $"  {nameof(Position_y)} = {Position_y}\n" +
                   $"  {nameof(Position_z)} = {Position_z}\n" +
                   $"  {nameof(Rotation_x)} = {Rotation_x}\n" +
                   $"  {nameof(Rotation_y)} = {Rotation_y}\n" +
                   $"  {nameof(Rotation_z)} = {Rotation_z}\n" +
                   $"  {nameof(Rotation_w)} = {Rotation_w}\n";
        }
    }
}
