using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class RegionHeartbeatMetric : IPacket
    {
        public uint MessageId => Messages.ClientKafka.RegionHeartbeatMetric;

        public SanUUID OwnerPersonaID { get; set; }
        public float AverageFrameRate { get; set; }
        public float MinFrameRate { get; set; }
        public float MaxFrameRate { get; set; }
        public uint Headcount { get; set; }
        public string Grid { get; set; }
        public string OwnerPersonaHandle { get; set; }
        public string ExperienceHandle { get; set; }
        public string InstanceId { get; set; }
        public string BuildID { get; set; }
        public string LocationHandle { get; set; }
        public string SansarURI { get; set; }
        public string CompatVersion { get; set; }
        public string ProtoVersion { get; set; }
        public string AccessGroup { get; set; }
        public string Configuration { get; set; }
        public string WorldId { get; set; }

        public RegionHeartbeatMetric(SanUUID ownerPersonaID, float averageFrameRate, float minFrameRate, float maxFrameRate, uint headcount, string grid, string ownerPersonaHandle, string experienceHandle, string instanceId, string buildID, string locationHandle, string sansarURI, string compatVersion, string protoVersion, string accessGroup, string configuration, string worldId)
        {
            OwnerPersonaID = ownerPersonaID;
            AverageFrameRate = averageFrameRate;
            MinFrameRate = minFrameRate;
            MaxFrameRate = maxFrameRate;
            Headcount = headcount;
            Grid = grid;
            OwnerPersonaHandle = ownerPersonaHandle;
            ExperienceHandle = experienceHandle;
            InstanceId = instanceId;
            BuildID = buildID;
            LocationHandle = locationHandle;
            SansarURI = sansarURI;
            CompatVersion = compatVersion;
            ProtoVersion = protoVersion;
            AccessGroup = accessGroup;
            Configuration = configuration;
            WorldId = worldId;
        }

        public RegionHeartbeatMetric(BinaryReader br)
        {
            OwnerPersonaID = br.ReadSanUUID();
            AverageFrameRate = br.ReadSingle();
            MinFrameRate = br.ReadSingle();
            MaxFrameRate = br.ReadSingle();
            Headcount = br.ReadUInt32();
            Grid = br.ReadSanString();
            OwnerPersonaHandle = br.ReadSanString();
            ExperienceHandle = br.ReadSanString();
            InstanceId = br.ReadSanString();
            BuildID = br.ReadSanString();
            LocationHandle = br.ReadSanString();
            SansarURI = br.ReadSanString();
            CompatVersion = br.ReadSanString();
            ProtoVersion = br.ReadSanString();
            AccessGroup = br.ReadSanString();
            Configuration = br.ReadSanString();
            WorldId = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(OwnerPersonaID);
                    bw.Write(AverageFrameRate);
                    bw.Write(MinFrameRate);
                    bw.Write(MaxFrameRate);
                    bw.Write(Headcount);
                    bw.WriteSanString(Grid);
                    bw.WriteSanString(OwnerPersonaHandle);
                    bw.WriteSanString(ExperienceHandle);
                    bw.WriteSanString(InstanceId);
                    bw.WriteSanString(BuildID);
                    bw.WriteSanString(LocationHandle);
                    bw.WriteSanString(SansarURI);
                    bw.WriteSanString(CompatVersion);
                    bw.WriteSanString(ProtoVersion);
                    bw.WriteSanString(AccessGroup);
                    bw.WriteSanString(Configuration);
                    bw.WriteSanString(WorldId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::RegionHeartbeatMetric:\n" +
                   $"  {nameof(OwnerPersonaID)} = {OwnerPersonaID}\n" +
                   $"  {nameof(AverageFrameRate)} = {AverageFrameRate}\n" +
                   $"  {nameof(MinFrameRate)} = {MinFrameRate}\n" +
                   $"  {nameof(MaxFrameRate)} = {MaxFrameRate}\n" +
                   $"  {nameof(Headcount)} = {Headcount}\n" +
                   $"  {nameof(Grid)} = {Grid}\n" +
                   $"  {nameof(OwnerPersonaHandle)} = {OwnerPersonaHandle}\n" +
                   $"  {nameof(ExperienceHandle)} = {ExperienceHandle}\n" +
                   $"  {nameof(InstanceId)} = {InstanceId}\n" +
                   $"  {nameof(BuildID)} = {BuildID}\n" +
                   $"  {nameof(LocationHandle)} = {LocationHandle}\n" +
                   $"  {nameof(SansarURI)} = {SansarURI}\n" +
                   $"  {nameof(CompatVersion)} = {CompatVersion}\n" +
                   $"  {nameof(ProtoVersion)} = {ProtoVersion}\n" +
                   $"  {nameof(AccessGroup)} = {AccessGroup}\n" +
                   $"  {nameof(Configuration)} = {Configuration}\n" +
                   $"  {nameof(WorldId)} = {WorldId}\n";
        }
    }

}
