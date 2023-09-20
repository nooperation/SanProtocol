using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class SetCharacterNodePhysics : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.SetCharacterNodePhysics;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public byte NodeType { get; set; }
        public byte CollisionsEnabled { get; set; }
        public byte BroadcastToSelf { get; set; }

        public SetCharacterNodePhysics(ulong frame, uint agentControllerId, byte nodeType, byte collisionsEnabled, byte broadcastToSelf)
        {
            this.Frame = frame;
            this.AgentControllerId = agentControllerId;
            this.NodeType = nodeType;
            this.CollisionsEnabled = collisionsEnabled;
            this.BroadcastToSelf = broadcastToSelf;
        }

        public SetCharacterNodePhysics(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            NodeType = br.ReadByte();
            CollisionsEnabled = br.ReadByte();
            BroadcastToSelf = br.ReadByte();
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
                    bw.Write(NodeType);
                    bw.Write(CollisionsEnabled);
                    bw.Write(BroadcastToSelf);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::SetCharacterNodePhysics:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(NodeType)} = {NodeType}\n" +
                   $"  {nameof(CollisionsEnabled)} = {CollisionsEnabled}\n" +
                   $"  {nameof(BroadcastToSelf)} = {BroadcastToSelf}\n";
        }
    }
}
