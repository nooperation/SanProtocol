using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class SetAgentController : IPacket
    {
        public uint MessageId => Messages.ClientRegion.SetAgentController;

        public uint AgentControllerId { get; set; }
        public ulong Frame { get; set; }

        public SetAgentController(uint agentControllerId, ulong frame)
        {
            AgentControllerId = agentControllerId;
            Frame = frame;
        }

        public SetAgentController(BinaryReader br)
        {
            AgentControllerId = br.ReadUInt32();
            Frame = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AgentControllerId);
                    bw.Write(Frame);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::SetAgentController:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n";
        }
    }

}
