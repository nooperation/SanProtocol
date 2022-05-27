using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class DetachFromCharacterNode : IPacket
    {
        public virtual uint MessageId => Messages.AgentController.DetachFromCharacterNode;

        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public uint AgentControllerId { get; set; }
        public List<float> BodyPosition { get; set; } = new List<float>();
        public List<float> BotyOrientation { get; set; } = new List<float>();
        public List<float> BodyVelocity { get; set; } = new List<float>();
        public List<float> BodyAngularVelocity { get; set; } = new List<float>();
        public byte NodeType { get; set; }

        public DetachFromCharacterNode(ulong frame, ulong componentId, uint agentControllerId, List<float> bodyPosition, List<float> botyOrientation, List<float> bodyVelocity, List<float> bodyAngularVelocity, byte nodeType)
        {
            this.Frame = frame;
            this.ComponentId = componentId;
            this.AgentControllerId = agentControllerId;
            this.BodyPosition = bodyPosition;
            this.BotyOrientation = botyOrientation;
            this.BodyVelocity = bodyVelocity;
            this.BodyAngularVelocity = bodyAngularVelocity;
            this.NodeType = nodeType;
        }

        public DetachFromCharacterNode(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                BodyPosition.Add(item);
            }
            for (var i = 0; i < 4; ++i)
            {
                var item = br.ReadSingle();
                BotyOrientation.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                BodyVelocity.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                BodyAngularVelocity.Add(item);
            }
            NodeType = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(ComponentId);
                    bw.Write(AgentControllerId);
                    foreach (var item in BodyPosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in BotyOrientation)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in BodyVelocity)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in BodyAngularVelocity)
                    {
                        bw.Write(item);
                    }
                    bw.Write(NodeType);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::DetachFromCharacterNode:\n" +
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
