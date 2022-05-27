using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.EditServer
{
    public class EditServerSpawnReady : IPacket
    {
        public uint MessageId => Messages.EditServer.EditServerSpawnReady;

        public byte IsValid { get; set; }
        public uint Serial { get; set; }
        public uint InstanceCount { get; set; }
        public string InventoryName { get; set; }
        public List<string> InstanceNames { get; set; } = new List<string>();
        public List<string> FolderNames { get; set; } = new List<string>();

        public EditServerSpawnReady(byte isValid, uint serial, uint instanceCount, string inventoryName, List<string> instanceNames, List<string> folderNames)
        {
            this.IsValid = isValid;
            this.Serial = serial;
            this.InstanceCount = instanceCount;
            this.InventoryName = inventoryName;
            this.InstanceNames = instanceNames;
            this.FolderNames = folderNames;
        }

        public EditServerSpawnReady(BinaryReader br)
        {
            IsValid = br.ReadByte();
            Serial = br.ReadUInt32();
            InstanceCount = br.ReadUInt32();
            InventoryName = br.ReadSanString();
            var numInstanceNames = br.ReadUInt32();
            for (var i = 0; i < numInstanceNames; ++i)
            {
                var str = br.ReadSanString();
                InstanceNames.Add(str);
            }
            var numFolderNames = br.ReadUInt32();
            for (var i = 0; i < numFolderNames; ++i)
            {
                var str = br.ReadSanString();
                FolderNames.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(IsValid);
                    bw.Write(Serial);
                    bw.Write(InstanceCount);
                    bw.WriteSanString(InventoryName);
                    bw.Write(InstanceNames.Count);
                    foreach (var item in InstanceNames)
                    {
                        bw.WriteSanString(item);
                    }
                    bw.Write(FolderNames.Count);
                    foreach (var item in FolderNames)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::EditServerSpawnReady:\n" +
                   $"  {nameof(IsValid)} = {IsValid}\n" +
                   $"  {nameof(Serial)} = {Serial}\n" +
                   $"  {nameof(InstanceCount)} = {InstanceCount}\n" +
                   $"  {nameof(InventoryName)} = {InventoryName}\n" +
                   $"  {nameof(InstanceNames)} = {String.Join(',', InstanceNames)}\n" +
                   $"  {nameof(FolderNames)} = {String.Join(',', FolderNames)}\n";
        }
    }
}
