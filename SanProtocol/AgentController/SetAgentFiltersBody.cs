using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class SetAgentFiltersBody : IPacket
    {
        public virtual uint MessageId => Messages.AgentController.SetAgentFiltersBody;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public ulong ComponentId { get; set; }
        public byte FilterBody { get; set; }

        public SetAgentFiltersBody(ulong frame, uint agentControllerId, ulong componentId, byte filterBody)
        {
            this.Frame = frame;
            this.AgentControllerId = agentControllerId;
            this.ComponentId = componentId;
            this.FilterBody = filterBody;
        }

        public SetAgentFiltersBody(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ComponentId = br.ReadUInt64();
            FilterBody = br.ReadByte();
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
                    bw.Write(FilterBody);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::SetAgentFiltersBody:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(FilterBody)} = {FilterBody}\n";
        }
    }
}
