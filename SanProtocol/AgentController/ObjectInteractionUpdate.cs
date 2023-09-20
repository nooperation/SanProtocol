using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class ObjectInteractionUpdate : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.ObjectInteractionUpdate;

        public ulong Frame { get; set; }
        public uint ClusterId { get; set; }
        public uint ObjectId { get; set; }
        public byte Enabled { get; set; }

        public ObjectInteractionUpdate(ulong frame, uint clusterId, uint objectId, byte enabled)
        {
            this.Frame = frame;
            this.ClusterId = clusterId;
            this.ObjectId = objectId;
            this.Enabled = enabled;
        }

        public ObjectInteractionUpdate(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ClusterId = br.ReadUInt32();
            ObjectId = br.ReadUInt32();
            Enabled = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(ClusterId);
                    bw.Write(ObjectId);
                    bw.Write(Enabled);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::ObjectInteractionUpdate:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n" +
                   $"  {nameof(ObjectId)} = {ObjectId}\n" +
                   $"  {nameof(Enabled)} = {Enabled}\n";
        }
    }
}
