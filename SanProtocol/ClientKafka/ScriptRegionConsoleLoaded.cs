using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class ScriptRegionConsoleLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafka.ScriptRegionConsoleLoaded;

        public string InstanceId { get; set; }
        public ulong Offset { get; set; }

        public ScriptRegionConsoleLoaded(string instanceId, ulong offset)
        {
            InstanceId = instanceId;
            Offset = offset;
        }

        public ScriptRegionConsoleLoaded(BinaryReader br)
        {
            InstanceId = br.ReadSanString();
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(InstanceId);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::ScriptRegionConsoleLoaded:\n" +
                   $"  {nameof(InstanceId)} = {InstanceId}\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
