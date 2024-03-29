﻿namespace SanProtocol.EditServer
{
    public class EditWorkspaceCommand : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.EditWorkspaceCommand;

        public byte[] CommandData { get; set; }

        public EditWorkspaceCommand(byte[] commandData)
        {
            CommandData = commandData;
        }

        public EditWorkspaceCommand(BinaryReader br)
        {
            var commandDataLength = br.ReadInt32();
            CommandData = br.ReadBytes(commandDataLength);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(CommandData.Length);
                    bw.Write(CommandData);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::EditWorkspaceCommand:\n" +
                   $"  {nameof(CommandData)} = {CommandData}\n";
        }
    }
}
