using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class SetCharacterUserProperty : IPacket
    {
        public uint MessageId => Messages.AgentController.SetCharacterUserProperty;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public float Value { get; set; }
        public byte PropertyType { get; set; }

        public SetCharacterUserProperty(ulong frame, uint agentControllerId, float value, byte propertyType)
        {
            this.Frame = frame;
            this.AgentControllerId = agentControllerId;
            this.Value = value;
            this.PropertyType = propertyType;
        }

        public SetCharacterUserProperty(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            Value = br.ReadSingle();
            PropertyType = br.ReadByte();
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
                    bw.Write(Value);
                    bw.Write(PropertyType);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::SetCharacterUserProperty:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Value)} = {Value}\n" +
                   $"  {nameof(PropertyType)} = {PropertyType}\n";
        }
    }
}
