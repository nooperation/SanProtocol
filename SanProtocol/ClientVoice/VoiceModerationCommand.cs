using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientVoice
{
    public class VoiceModerationCommand : IPacket
    {
        public uint MessageId => Messages.ClientVoice.VoiceModerationCommand;

        public string CommandLine { get; set; }

        public VoiceModerationCommand(string commandLine)
        {
            CommandLine = commandLine;
        }

        public VoiceModerationCommand(BinaryReader br)
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
            return $"ClientVoice::VoiceModerationCommand:\n" +
                   $"  {nameof(CommandLine)} = {CommandLine}\n";
        }
    }

}
