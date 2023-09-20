using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class ObjectInteractionCreate : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.ObjectInteractionCreate;

        public ulong Frame { get; set; }
        public uint ClusterId { get; set; }
        public uint ObjectId { get; set; }
        public string Prompt { get; set; }
        public byte Enabled { get; set; }

        public ObjectInteractionCreate(ulong frame, uint clusterId, uint objectId, string prompt, byte enabled)
        {
            this.Frame = frame;
            this.ClusterId = clusterId;
            this.ObjectId = objectId;
            this.Prompt = prompt;
            this.Enabled = enabled;
        }

        public ObjectInteractionCreate(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ClusterId = br.ReadUInt32();
            ObjectId = br.ReadUInt32();
            Prompt = br.ReadSanString();
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
                    bw.WriteSanString(Prompt);
                    bw.Write(Enabled);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::ObjectInteractionCreate:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n" +
                   $"  {nameof(ObjectId)} = {ObjectId}\n" +
                   $"  {nameof(Prompt)} = {Prompt}\n" +
                   $"  {nameof(Enabled)} = {Enabled}\n";
        }
    }
}
