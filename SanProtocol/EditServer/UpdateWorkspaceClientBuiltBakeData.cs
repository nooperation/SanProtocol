﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class UpdateWorkspaceClientBuiltBakeData : IPacket
    {
        public uint MessageId => Messages.EditServer.UpdateWorkspaceClientBuiltBakeData;

        public string Authorization { get; set; }
        public byte[] BakeData { get; set; }

        public UpdateWorkspaceClientBuiltBakeData(string authorization, byte[] bakeData)
        {
            this.Authorization = authorization;
            this.BakeData = bakeData;
        }

        public UpdateWorkspaceClientBuiltBakeData(BinaryReader br)
        {
            Authorization = br.ReadSanString();
            var bakeDataLength = br.ReadInt32();
            BakeData = br.ReadBytes(bakeDataLength);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Authorization);
                    bw.Write(BakeData.Length);
                    bw.Write(BakeData);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::UpdateWorkspaceClientBuiltBakeData:\n" +
                   $"  {nameof(Authorization)} = {Authorization}\n" +
                   $"  {nameof(BakeData)} = {BakeData}\n";
        }
    }
}
