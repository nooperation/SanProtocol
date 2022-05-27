using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class UnsubscribeCommand : IPacket
    {
        public uint MessageId => Messages.ClientRegion.UnsubscribeCommand;

        public byte Action { get; set; }
        public string Command { get; set; }

        public UnsubscribeCommand(byte action, string command)
        {
            Action = action;
            Command = command;
        }

        public UnsubscribeCommand(BinaryReader br)
        {
            Action = br.ReadByte();
            Command = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Action);
                    bw.WriteSanString(Command);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UnsubscribeCommand:\n" +
                   $"  {nameof(Action)} = {Action}\n" +
                   $"  {nameof(Command)} = {Command}\n";
        }
    }

}
