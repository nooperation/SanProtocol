using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class UnsubscribeScriptRegionConsole : IPacket
    {
        public uint MessageId => Messages.ClientKafka.UnsubscribeScriptRegionConsole;

        public string InstanceId { get; set; }

        public UnsubscribeScriptRegionConsole(string instanceId)
        {
            InstanceId = instanceId;
        }

        public UnsubscribeScriptRegionConsole(BinaryReader br)
        {
            InstanceId = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(InstanceId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::UnsubscribeScriptRegionConsole:\n" +
                   $"  {nameof(InstanceId)} = {InstanceId}\n";
        }
    }

}
