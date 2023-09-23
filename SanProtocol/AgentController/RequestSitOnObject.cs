namespace SanProtocol.AgentController
{
    public class RequestSitOnObject : SitOnObject
    {
        public override uint MessageId => Messages.AgentControllerMessages.RequestSitOnObject;

        public RequestSitOnObject(ulong frame, uint agentControllerId, ulong componentId)
            : base(
                 frame,
                 agentControllerId,
                 componentId
            )
        {
        }

        public RequestSitOnObject(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"AgentController::RequestSitOnObject:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n";
        }
    }
}
