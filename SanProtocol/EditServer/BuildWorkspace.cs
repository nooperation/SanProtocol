using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class BuildWorkspace : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.BuildWorkspace;

        public string Authorization { get; set; }
        public string SceneName { get; set; }
        public byte Start { get; set; }

        public BuildWorkspace(string authorization, string sceneName, byte start)
        {
            this.Authorization = authorization;
            this.SceneName = sceneName;
            this.Start = start;
        }

        public BuildWorkspace(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            SceneName = br.ReadSanString();
            Start = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.WriteSanString(SceneName);
                    bw.Write(Start);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::BuildWorkspace:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(SceneName)} = {SceneName}\n" +
                   $"  {nameof(Start)} = {Start}\n";
        }
    }
}
