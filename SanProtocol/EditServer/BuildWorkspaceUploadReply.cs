using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class BuildWorkspaceUploadReply : IPacket
    {
        public uint MessageId => Messages.EditServer.BuildWorkspaceUploadReply;

        public byte Success { get; set; }
        public string WorldDefinitionId { get; set; }

        public BuildWorkspaceUploadReply(byte success, string worldDefinitionId)
        {
            this.Success = success;
            this.WorldDefinitionId = worldDefinitionId;
        }

        public BuildWorkspaceUploadReply(BinaryReader br)
        {
            Success = br.ReadByte();
            WorldDefinitionId = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Success);
                    bw.WriteSanString(WorldDefinitionId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::BuildWorkspaceUploadReply:\n" +
                   $"  {nameof(Success)} = {Success}\n" +
                   $"  {nameof(WorldDefinitionId)} = {WorldDefinitionId}\n";
        }
    }
}
