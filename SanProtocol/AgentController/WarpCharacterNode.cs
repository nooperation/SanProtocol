﻿namespace SanProtocol.AgentController
{
    public class WarpCharacterNode : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.WarpCharacterNode;

        public uint AgentControllerId { get; set; }
        public uint NodeType { get; set; }

        public WarpCharacterNode(uint agentControllerId, uint nodeType)
        {
            AgentControllerId = agentControllerId;
            NodeType = nodeType;
        }

        public WarpCharacterNode(BinaryReader br)
        {
            AgentControllerId = br.ReadUInt32();
            NodeType = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AgentControllerId);
                    bw.Write(NodeType);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::WarpCharacterNode:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(NodeType)} = {NodeType}\n";
        }
    }
}
