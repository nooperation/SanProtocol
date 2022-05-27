using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class RequestDetachFromCharacterNode : DetachFromCharacterNode
    {
        public override uint MessageId => Messages.AgentController.RequestDetachFromCharacterNode;

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
            :base(br)
        {
        }


        public override string ToString()
        {
            return $"AgentController::RequestDetachFromCharacterNode:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(BodyPosition)} = <{String.Join(',', BodyPosition)}>\n" +
                   $"  {nameof(BotyOrientation)} = <{String.Join(',', BotyOrientation)}>\n" +
                   $"  {nameof(BodyVelocity)} = <{String.Join(',', BodyVelocity)}>\n" +
                   $"  {nameof(BodyAngularVelocity)} = <{String.Join(',', BodyAngularVelocity)}>\n" +
                   $"  {nameof(NodeType)} = {NodeType}\n";
        }
    }
}
