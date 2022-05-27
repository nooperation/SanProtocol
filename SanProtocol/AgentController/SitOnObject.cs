using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class SitOnObject : IPacket
    {
        public virtual uint MessageId => Messages.AgentController.SitOnObject;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public ulong ComponentId { get; set; }
        public byte OwnershipWatermark { get; set; }
        public byte SkipAnimation { get; set; }

        public SitOnObject(ulong frame, uint agentControllerId, ulong componentId, byte ownershipWatermark, byte skipAnimation)
        {
            this.Frame = frame;
            this.AgentControllerId = agentControllerId;
            this.ComponentId = componentId;
            this.OwnershipWatermark = ownershipWatermark;
            this.SkipAnimation = skipAnimation;
        }

        public SitOnObject(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ComponentId = br.ReadUInt64();
            OwnershipWatermark = br.ReadByte();
            SkipAnimation = br.ReadByte();
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
                    bw.Write(OwnershipWatermark);
                    bw.Write(SkipAnimation);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::SitOnObject:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(OwnershipWatermark)} = {OwnershipWatermark}\n" +
                   $"  {nameof(SkipAnimation)} = {SkipAnimation}\n";
        }
    }
}
