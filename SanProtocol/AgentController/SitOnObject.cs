﻿namespace SanProtocol.AgentController
{
    public class SitOnObject : IPacket
    {
        public virtual uint MessageId => Messages.AgentControllerMessages.SitOnObject;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public ulong ComponentId { get; set; }

        public SitOnObject(ulong frame, uint agentControllerId, ulong componentId)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            ComponentId = componentId;
        }

        public SitOnObject(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ComponentId = br.ReadUInt64();
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
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::SitOnObject:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n";
        }
    }
}
