namespace SanProtocol.AgentController
{
    public class CharacterControllerInputReliable : CharacterControllerInput
    {
        public override uint MessageId => Messages.AgentControllerMessages.CharacterControllerInputReliable;

        public CharacterControllerInputReliable(ulong frame, uint agentControllerId, byte jumpState, byte jumpBtnPressed, float moveRight, float moveForward, float cameraYaw, float cameraPitch, float behaviorYawDelta, float behaviorPitchDelta, float characterForward, Quaternion cameraForward)
            : base(
                frame,
                agentControllerId,
                jumpState,
                jumpBtnPressed,
                moveRight,
                moveForward,
                cameraYaw,
                cameraPitch,
                behaviorYawDelta,
                behaviorPitchDelta,
                characterForward,
                cameraForward
            )
        {

        }

        public CharacterControllerInputReliable(BinaryReader br)
            : base(br)
        {
        }


        public override string ToString()
        {
            return $"Simulation::CharacterControllerInputReliable:\n" +
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
