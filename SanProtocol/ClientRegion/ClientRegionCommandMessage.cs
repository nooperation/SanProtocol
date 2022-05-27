using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ClientRegionCommandMessage : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientRegionCommandMessage;

        public string CommandLine { get; set; }

        public ClientRegionCommandMessage(string commandLine)
        {
            CommandLine = commandLine;
        }

        public ClientRegionCommandMessage(BinaryReader br)
        {
            CommandLine = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(CommandLine);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientRegionCommandMessage:\n" +
                   $"  {nameof(CommandLine)} = {CommandLine}\n";
        }
    }

}
