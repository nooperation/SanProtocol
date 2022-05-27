using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class RequestSetAgentFiltersBody : SetAgentFiltersBody
    {
        public override uint MessageId => Messages.AgentController.RequestSetAgentFiltersBody;

        public RequestSetAgentFiltersBody(ulong frame, uint agentControllerId, ulong componentId, byte filterBody)
            :base (frame, agentControllerId, componentId, filterBody)
        {
        }

        public RequestSetAgentFiltersBody(BinaryReader br)
            :base(br)
        {
        }

        public override string ToString()
        {
            return $"AgentController::RequestSetAgentFiltersBody:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(FilterBody)} = {FilterBody}\n";
        }
    }
}
