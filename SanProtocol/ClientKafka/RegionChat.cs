using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class RegionChat : IPacket
    {
        public uint MessageId => Messages.ClientKafka.RegionChat;

        public SanUUID FromPersonaId { get; set; }
        public SanUUID ToPersonaId { get; set; }
        public string InstanceAddress { get; set; }
        public uint AgentControllerId { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }
        public byte Typing { get; set; }
        public ulong Offset { get; set; }
        public ulong HighwaterMarkOffset { get; set; }

        public RegionChat(SanUUID fromPersonaId, SanUUID toPersonaId, string instanceAddress, uint agentControllerId, string message, long timestamp, byte typing, ulong offset, ulong highwaterMarkOffset)
        {
            FromPersonaId = fromPersonaId;
            ToPersonaId = toPersonaId;
            InstanceAddress = instanceAddress;
            AgentControllerId = agentControllerId;
            Message = message;
            Timestamp = timestamp;
            Typing = typing;
            Offset = offset;
            HighwaterMarkOffset = highwaterMarkOffset;
        }

        public RegionChat(BinaryReader br)
        {
            FromPersonaId = br.ReadSanUUID();
            ToPersonaId = br.ReadSanUUID();
            InstanceAddress = br.ReadSanString();
            AgentControllerId = br.ReadUInt32();
            Message = br.ReadSanString();
            Timestamp = br.ReadInt64();
            Typing = br.ReadByte();
            Offset = br.ReadUInt64();
            HighwaterMarkOffset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(FromPersonaId);
                    bw.Write(ToPersonaId);
                    bw.WriteSanString(InstanceAddress);
                    bw.Write(AgentControllerId);
                    bw.WriteSanString(Message);
                    bw.Write(Timestamp);
                    bw.Write(Typing);
                    bw.Write(Offset);
                    bw.Write(HighwaterMarkOffset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::RegionChat:\n" +
                   $"  {nameof(FromPersonaId)} = {FromPersonaId}\n" +
                   $"  {nameof(ToPersonaId)} = {ToPersonaId}\n" +
                   $"  {nameof(InstanceAddress)} = {InstanceAddress}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Message)} = {Message}\n" +
                   $"  {nameof(Timestamp)} = {Timestamp}\n" +
                   $"  {nameof(Typing)} = {Typing}\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(HighwaterMarkOffset)} = {HighwaterMarkOffset}\n";
        }
    }
}
