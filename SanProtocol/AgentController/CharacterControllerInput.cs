namespace SanProtocol.AgentController
{
    public class CharacterControllerInput : IPacket
    {
        public virtual uint MessageId => Messages.AgentControllerMessages.CharacterControllerInput;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public byte JumpState { get; set; }
        public byte JumpBtnPressed { get; set; }
        public float MoveRight { get; set; }
        public float MoveForward { get; set; }
        public float CameraYaw { get; set; }
        public float CameraPitch { get; set; }
        public float BehaviorYawDelta { get; set; }
        public float BehaviorPitchDelta { get; set; }
        public float CharacterForward { get; set; }
        public Quaternion CameraForward { get; set; }

        public CharacterControllerInput(
            ulong frame,
            uint agentControllerId,
            byte jumpState,
            byte jumpBtnPressed,
            float moveRight,
            float moveForward,
            float cameraYaw,
            float cameraPitch,
            float behaviorYawDelta,
            float behaviorPitchDelta,
            float characterForward,
            Quaternion cameraForward)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            JumpState = jumpState;
            JumpBtnPressed = jumpBtnPressed;
            MoveRight = moveRight;
            MoveForward = moveForward;
            CameraYaw = cameraYaw;
            CameraPitch = cameraPitch;
            BehaviorYawDelta = behaviorYawDelta;
            BehaviorPitchDelta = behaviorPitchDelta;
            CharacterForward = characterForward;
            CameraForward = cameraForward;
        }

        public CharacterControllerInput(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            JumpState = br.ReadByte();
            JumpBtnPressed = br.ReadByte();

            var bitReader = new BitReader(br);
            MoveRight = bitReader.ReadFloat(12, 1.0f);
            MoveForward = bitReader.ReadFloat(12, 1.0f);
            CameraYaw = bitReader.ReadFloat(13, 64.0f);
            CameraPitch = bitReader.ReadFloat(13, 64.0f);
            BehaviorYawDelta = bitReader.ReadFloat(11, 10.0f);
            BehaviorPitchDelta = bitReader.ReadFloat(11, 10.0f);
            CharacterForward = bitReader.ReadFloat(15, 4.0f);
            CameraForward = bitReader.ReadQuaternion(2, 12);
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
                    bw.Write(JumpState);
                    bw.Write(JumpBtnPressed);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloat(MoveRight, 12, 1.0f);
                    bitWriter.WriteFloat(MoveForward, 12, 1.0f);
                    bitWriter.WriteFloat(CameraYaw, 13, 64.0f);
                    bitWriter.WriteFloat(CameraPitch, 13, 64.0f);
                    bitWriter.WriteFloat(BehaviorYawDelta, 11, 10.0f);
                    bitWriter.WriteFloat(BehaviorPitchDelta, 11, 10.0f);
                    bitWriter.WriteFloat(CharacterForward, 15, 4.0f);
                    bitWriter.WriteQuaternion(CameraForward, 12);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Simulation::CharacterControllerInput:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(JumpState)} = {JumpState}\n" +
                   $"  {nameof(JumpBtnPressed)} = {JumpBtnPressed}\n" +
                   $"  {nameof(MoveRight)} = {MoveRight}\n" +
                   $"  {nameof(MoveForward)} = {MoveForward}\n" +
                   $"  {nameof(CameraYaw)} = {CameraYaw}\n" +
                   $"  {nameof(CameraPitch)} = {CameraPitch}\n" +
                   $"  {nameof(BehaviorYawDelta)} = {BehaviorYawDelta}\n" +
                   $"  {nameof(BehaviorPitchDelta)} = {BehaviorPitchDelta}\n" +
                   $"  {nameof(CharacterForward)} = {CharacterForward}\n" +
                   $"  {nameof(CameraForward)} = {CameraForward}\n";
        }
    }

}
