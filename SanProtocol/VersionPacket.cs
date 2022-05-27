using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace SanBot.Packets
{
    public class VersionPacket : IPacket
    {
        public enum VersionType
        {
            ClientKafkaChannel,
            ClientVoiceChannel,
            ClientRegionChannel
        };

        public uint MessageId => 0x00000000;

        public uint Unknown { get; set; }
        public string Name { get; set; }
        Dictionary<uint, ushort> Versions = new Dictionary<uint, ushort>();

        public VersionPacket(VersionType versionType)
        {
            if (versionType == VersionType.ClientKafkaChannel)
            {
                Name = versionType.ToString();
                Unknown = 1;
                Versions = new Dictionary<uint, ushort>()
                {
                      {0x08445006, 1},
                      {0x0AF50C12, 1},
                      {0x078DCC26, 1},
                      {0xD7C7DC26, 1},
                      {0x2DC9B029, 1},
                      {0xA685E82B, 1},
                      {0xF5361468, 1},
                      {0xAD589C6F, 1},
                      {0x3BFA4474, 1},
                      {0xB11C8C84, 1},
                      {0x3494608D, 1},
                      {0xDCF900A4, 1},
                      {0x203CC0A8, 1},
                      {0xD49B04C3, 1},
                      {0xE3466906, 1},
                      {0x14FFCD37, 1},
                      {0x59CF6950, 1},
                      {0x00B0E15E, 1},
                      {0xD3CAA979, 1},
                      {0x0C0C9D81, 1},
                      {0xE24EBDD3, 1},
                      {0x1DB989E8, 1},
                      {0x955C35EB, 1},
                      {0x46C5FDF3, 1},
                      {0x650939F7, 1},
                      {0x22565685, 1},
                      {0x0A7562A7, 1},
                      {0xE4ADC2EB, 1},
                      {0xBA6DB2FC, 1},
                      {0x4B73CF2C, 1},
                      {0x304D3746, 1},
                      {0xA2190F5D, 1},
                      {0x9BC4EF8A, 1},
                      {0x75BAFB95, 1},
                      {0x4AC30FE7, 1},
                      {0xA356B3ED, 1},
                      {0xB4AB87F5, 1},
                      {0x5915FBFE, 1},
                };
            }
            else if (versionType == VersionType.ClientVoiceChannel)
            {
                Name = versionType.ToString();
                Unknown = 1;
                Versions = new Dictionary<uint, ushort>()
                {
                      {0xA6972017, 1},
                      {0x0D50D087, 1},
                      {0x573EE089, 1},
                      {0x56800096, 1},
                      {0xC91B2D1C, 1},
                      {0x742CE528, 1},
                      {0x59AC5555, 1},
                      {0xD9306963, 1},
                      {0x3A168D81, 1},
                      {0x3F7171FB, 1},
                      {0x5A978A32, 1},
                      {0x88C28A79, 1},
                      {0x1798BA9C, 1},
                      {0xF2FB6AD0, 1},
                      {0x90DA7ED3, 1},
                      {0x47C4FFDF, 1}
                };
            }
            else if (versionType == VersionType.ClientRegionChannel)
            {
                Name = versionType.ToString();
                Unknown = 1;
                Versions = new Dictionary<uint, ushort>()
                {
                    {0xCA6CCC08, 1},
                    {0x3902800A, 1},
                    {0x58003034, 1},
                    {0x31D1EC43, 1},
                    {0x941E6445, 1},
                    {0xE5321C47, 1},
                    {0x36164050, 1},
                    {0x28F54053, 1},
                    {0x8FB6F456, 1},
                    {0x51A1705A, 1},
                    {0x065C105B, 1},
                    {0x5178DC5E, 1},
                    {0xB87F9C66, 1},
                    {0x75C0AC6B, 1},
                    {0xBB382C6B, 1},
                    {0x88023C72, 1},
                    {0x4F20B073, 1},
                    {0x3F020C77, 1},
                    {0xF8E77C8E, 1},
                    {0x2B87F09D, 1},
                    {0x8C738C9E, 1},
                    {0x3D490CAB, 1},
                    {0x16C090B1, 1},
                    {0xE945D8B8, 1},
                    {0x893A18BE, 1},
                    {0x191F08C0, 1},
                    {0x412484C4, 1},
                    {0x20EDD0C4, 1},
                    {0xABDA80C7, 1},
                    {0x981AB0D6, 1},
                    {0x982B98D8, 1},
                    {0x864418DA, 2},
                    {0x604E18DE, 1},
                    {0xEA2934E8, 1},
                    {0xEC3CA8EC, 1},
                    {0x6A2C4CEF, 1},
                    {0xA67454F0, 1},
                    {0x2DF35CF3, 1},
                    {0xC67C58F7, 1},
                    {0x2C21850D, 1},
                    {0x4B68A51C, 1},
                    {0x5DCD6123, 1},
                    {0x0B3B7D2E, 1},
                    {0x6188A537, 1},
                    {0xF6B9093E, 1},
                    {0xCE9B5148, 1},
                    {0x73810D53, 1},
                    {0x3A92215C, 1},
                    {0x1651CD68, 1},
                    {0xD22C9D73, 1},
                    {0x645C4976, 1},
                    {0x5C1A8D7D, 1},
                    {0x20C45982, 1},
                    {0x17B7D18A, 1},
                    {0xC33DE58B, 1},
                    {0xDDDEC199, 1},
                    {0xBAFD799D, 1},
                    {0x009385A0, 1},
                    {0xA25F81AB, 1},
                    {0x45C605B8, 1},
                    {0x0CC9F1B8, 1},
                    {0xF66AD9BF, 1},
                    {0x60C955C0, 1},
                    {0x371D99C1, 1},
                    {0x9643B9C3, 1},
                    {0x5749A1CD, 1},
                    {0x1E9B31CE, 1},
                    {0x866BF5CF, 1},
                    {0xDE87FDD8, 1},
                    {0xC11AFDE7, 1},
                    {0x91419DEB, 1},
                    {0x0D3809EB, 1},
                    {0x4C1B3DF2, 1},
                    {0x3EB3EDF7, 1},
                    {0x5C7CC1FC, 1},
                    {0x1A5C9610, 1},
                    {0x41FE0612, 1},
                    {0x53078A1E, 1},
                    {0x0A7FC621, 1},
                    {0xF555FE2D, 5},
                    {0x4DB48E35, 1},
                    {0x64225637, 1},
                    {0x575AC239, 1},
                    {0xB34F3A45, 1},
                    {0x2926D248, 1},
                    {0x23314E53, 1},
                    {0x349AD257, 2},
                    {0x7BB86A5B, 1},
                    {0x85BA6E75, 1},
                    {0xEB3C4296, 1},
                    {0x0B617A9A, 1},
                    {0xBB086E9B, 1},
                    {0x0741CA9B, 2},
                    {0x67B63AA3, 1},
                    {0x7E28AEAF, 1},
                    {0x34793AB0, 1},
                    {0xAB2F1EB1, 1},
                    {0x45FAAEBC, 1},
                    {0x083642BD, 1},
                    {0x217192BE, 1},
                    {0x6F5546CE, 1},
                    {0x30CDBED6, 1},
                    {0x1505C6D8, 1},
                    {0xE4C496DF, 1},
                    {0x6951DAEC, 1},
                    {0xECE56EFD, 1},
                    {0x559B7F04, 1},
                    {0x403D5704, 1},
                    {0x5F483F0C, 1},
                    {0x7D22C30C, 1},
                    {0x8FC77316, 1},
                    {0xAE522F17, 1},
                    {0xFCA3EF20, 1},
                    {0xA5C4FB23, 1},
                    {0xD6F4CF23, 1},
                    {0x53A4BF26, 1},
                    {0x80F90328, 1},
                    {0x0D938F45, 2},
                    {0x5489A347, 1},
                    {0x4C3B3B4B, 1},
                    {0x2BDBDB56, 1},
                    {0xE1EE5F5D, 1},
                    {0x685B436C, 1},
                    {0x7846436E, 1},
                    {0x4221836F, 1},
                    {0x87341F77, 1},
                    {0xB4E1AB7B, 1},
                    {0xEFC20B7F, 1},
                    {0x00AC2B81, 1},
                    {0xA36E9F9C, 1},
                    {0x16406FB7, 1},
                    {0x581827CC, 1},
                    {0xA7D6EFD1, 1},
                    {0x970F93D4, 1},
                    {0x32DC63D7, 3},
                    {0x83F1D7DB, 1},
                    {0x25C093E0, 1},
                    {0x0D094FEA, 1},
                    {0x86E6A7F6, 1},
                    {0x09DD53F6, 1},
                    {0x706F63FB, 1}
                };
            }
            else
            {
                throw new Exception("Invalid Version Type");
            }
        }

        public VersionPacket(BinaryReader br)
        {
            Unknown = br.ReadUInt32();
            Name = br.ReadSanString();
            Versions = new Dictionary<uint, ushort>();

            var numEntries = br.ReadInt32();
            for (int i = 0; i < numEntries; i++)
            {
                var id = br.ReadUInt32();
                var version = br.ReadUInt16();

                Versions[id] = version;
            }
        }

        public byte[] GetBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Unknown);
                    bw.Write(Name.Length);
                    bw.Write(Name.ToCharArray());
                    bw.Write(Versions.Count);
                    foreach (var item in Versions)
                    {
                        bw.Write(item.Key);
                        bw.Write(item.Value);
                    }
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            var str =
                $"{nameof(Name)} = {Name}\n";

            foreach (var item in Versions)
            {
                str += $"  0x{item.Key:X8} = {item.Value}\n";
            }

            return str;
        }
    }
}
