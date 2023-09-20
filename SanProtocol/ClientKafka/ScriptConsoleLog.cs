using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class ScriptConsoleLog : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.ScriptConsoleLog;

        public uint LogLevel { get; set; }
        public string Tag { get; set; }
        public string Message { get; set; }
        public ulong Timestamp { get; set; }
        public uint ScriptId { get; set; }
        public string ScriptClassName { get; set; }
        public string WorldId { get; set; }
        public string InstanceId { get; set; }
        public SanUUID OwnerPersonaId { get; set; }
        public ulong Offset { get; set; }

        public ScriptConsoleLog(uint logLevel, string tag, string message, ulong timestamp, uint scriptId, string scriptClassName, string worldId, string instanceId, SanUUID ownerPersonaId, ulong offset)
        {
            LogLevel = logLevel;
            Tag = tag;
            Message = message;
            Timestamp = timestamp;
            ScriptId = scriptId;
            ScriptClassName = scriptClassName;
            WorldId = worldId;
            InstanceId = instanceId;
            OwnerPersonaId = ownerPersonaId;
            Offset = offset;
        }

        public ScriptConsoleLog(BinaryReader br)
        {
            LogLevel = br.ReadUInt32();
            Tag = br.ReadSanString();
            Message = br.ReadSanString();
            Timestamp = br.ReadUInt64();
            ScriptId = br.ReadUInt32();
            ScriptClassName = br.ReadSanString();
            WorldId = br.ReadSanString();
            InstanceId = br.ReadSanString();
            OwnerPersonaId = br.ReadSanUUID();
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(LogLevel);
                    bw.WriteSanString(Tag);
                    bw.WriteSanString(Message);
                    bw.Write(Timestamp);
                    bw.Write(ScriptId);
                    bw.WriteSanString(ScriptClassName);
                    bw.WriteSanString(WorldId);
                    bw.WriteSanString(InstanceId);
                    bw.Write(OwnerPersonaId);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::ScriptConsoleLog:\n" +
                   $"  {nameof(LogLevel)} = {LogLevel}\n" +
                   $"  {nameof(Tag)} = {Tag}\n" +
                   $"  {nameof(Message)} = {Message}\n" +
                   $"  {nameof(Timestamp)} = {Timestamp}\n" +
                   $"  {nameof(ScriptId)} = {ScriptId}\n" +
                   $"  {nameof(ScriptClassName)} = {ScriptClassName}\n" +
                   $"  {nameof(WorldId)} = {WorldId}\n" +
                   $"  {nameof(InstanceId)} = {InstanceId}\n" +
                   $"  {nameof(OwnerPersonaId)} = {OwnerPersonaId}\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }
}
