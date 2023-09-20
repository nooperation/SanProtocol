using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class SubscribeCommand : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.SubscribeCommand;

        public string Command { get; set; }
        public byte Action { get; set; }

        public SubscribeCommand(string command, byte action)
        {
            Command = command;
            Action = action;
        }

        public SubscribeCommand(BinaryReader br)
        {
            Command = br.ReadSanString();
            Action = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Command);
                    bw.Write(Action);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::SubscribeCommand:\n" +
                   $"  {nameof(Command)} = {Command}\n" +
                   $"  {nameof(Action)} = {Action}\n";
        }
    }

}
