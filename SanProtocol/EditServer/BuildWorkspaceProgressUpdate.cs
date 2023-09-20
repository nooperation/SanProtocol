using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class BuildWorkspaceProgressUpdate : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.BuildWorkspaceProgressUpdate;

        public uint Stage { get; set; }
        public float Percent { get; set; }

        public BuildWorkspaceProgressUpdate(uint stage, float percent)
        {
            this.Stage = stage;
            this.Percent = percent;
        }

        public BuildWorkspaceProgressUpdate(BinaryReader br)
        {
            Stage = br.ReadUInt32();
            Percent = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Stage);
                    bw.Write(Percent);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::BuildWorkspaceProgressUpdate:\n" +
                   $"  {nameof(Stage)} = {Stage}\n" +
                   $"  {nameof(Percent)} = {Percent}\n";
        }
    }
}
