namespace SanProtocol.AgentController
{
    public class RequestDetachFromCharacterNode : DetachFromCharacterNode
    {
        public override uint MessageId => Messages.AgentControllerMessages.RequestDetachFromCharacterNode;

        public RequestDetachFromCharacterNode(ulong frame, ulong componentId, uint agentControllerId, List<float> bodyPosition, List<float> botyOrientation, List<float> bodyVelocity, List<float> bodyAngularVelocity, byte nodeType)
            : base(
                frame,
                componentId,
                agentControllerId,
                bodyPosition,
                botyOrientation,
                bodyVelocity,
                bodyAngularVelocity,
                nodeType
            )
        {

        }

        public RequestDetachFromCharacterNode(BinaryReader br)
            : base(br)
        {
        }


        public override string ToString()
        {
            return $"AgentController::RequestDetachFromCharacterNode:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(BodyPosition)} = <{string.Join(',', BodyPosition)}>\n" +
                   $"  {nameof(BotyOrientation)} = <{string.Join(',', BotyOrientation)}>\n" +
                   $"  {nameof(BodyVelocity)} = <{string.Join(',', BodyVelocity)}>\n" +
                   $"  {nameof(BodyAngularVelocity)} = <{string.Join(',', BodyAngularVelocity)}>\n" +
                   $"  {nameof(NodeType)} = {NodeType}\n";
        }
    }
}
