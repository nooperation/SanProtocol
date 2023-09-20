using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class ObjectInteractionPromptUpdate : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.ObjectInteractionPromptUpdate;

        public ulong Frame { get; set; }
        public uint ClusterId { get; set; }
        public uint ObjectId { get; set; }
        public string Prompt { get; set; }

        public ObjectInteractionPromptUpdate(ulong frame, uint clusterId, uint objectId, string prompt)
        {
            this.Frame = frame;
            this.ClusterId = clusterId;
            this.ObjectId = objectId;
            this.Prompt = prompt;
        }

        public ObjectInteractionPromptUpdate(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ClusterId = br.ReadUInt32();
            ObjectId = br.ReadUInt32();
            Prompt = br.ReadSanString();
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
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::ObjectInteractionPromptUpdate:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n" +
                   $"  {nameof(ObjectId)} = {ObjectId}\n" +
                   $"  {nameof(Prompt)} = {Prompt}\n";
        }
    }
}
