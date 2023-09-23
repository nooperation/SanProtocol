namespace SanProtocol.ClientKafka
{
    public class InventoryItemUpdate : IPacket
    {
        public uint MessageId => Messages.ClientKafkaMessages.InventoryItemUpdate;

        public string Id { get; set; }
        public string Licensee_label { get; set; }
        public string Licensor_label { get; set; }
        public string Compat_version { get; set; }
        public string Licensor_pid { get; set; }
        public string CreationTime { get; set; }
        public string ModificationTime { get; set; }
        public uint Origin { get; set; }
        public string OriginReference { get; set; }
        public List<InventoryItemRevision> Revisions { get; set; } = new List<InventoryItemRevision>();
        public ulong Offset { get; set; }
        public byte State { get; set; }

        public InventoryItemUpdate(string id, string licensee_label, string licensor_label, string compat_version, string licensor_pid, string creationTime, string modificationTime, uint origin, string originReference, List<InventoryItemRevision> revisions, ulong offset, byte state)
        {
            Id = id;
            Licensee_label = licensee_label;
            Licensor_label = licensor_label;
            Compat_version = compat_version;
            Licensor_pid = licensor_pid;
            CreationTime = creationTime;
            ModificationTime = modificationTime;
            Origin = origin;
            OriginReference = originReference;
            Revisions = revisions;
            Offset = offset;
            State = state;
        }

        public InventoryItemUpdate(BinaryReader br)
        {
            Id = br.ReadSanString();
            Licensee_label = br.ReadSanString();
            Licensor_label = br.ReadSanString();
            Compat_version = br.ReadSanString();
            Licensor_pid = br.ReadSanString();
            CreationTime = br.ReadSanString();
            ModificationTime = br.ReadSanString();
            Origin = br.ReadUInt32();
            OriginReference = br.ReadSanString();

            var revisionsLength = br.ReadUInt32();
            for (var i = 0; i < revisionsLength; i++)
            {
                var newRevision = new InventoryItemRevision(br);
                Revisions.Add(newRevision);
            }

            Offset = br.ReadUInt64();
            State = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Id);
                    bw.WriteSanString(Licensee_label);
                    bw.WriteSanString(Licensor_label);
                    bw.WriteSanString(Compat_version);
                    bw.WriteSanString(Licensor_pid);
                    bw.WriteSanString(CreationTime);
                    bw.WriteSanString(ModificationTime);
                    bw.Write(Origin);
                    bw.WriteSanString(OriginReference);
                    bw.Write(Revisions.Count);
                    foreach (var item in Revisions)
                    {
                        // revisionBytes has a MessageID header, which we need to ignore here.
                        var revisionBytes = item.GetBytes();
                        revisionBytes = revisionBytes.Skip(4).ToArray();
                        bw.Write(revisionBytes);
                    }
                    bw.Write(Offset);
                    bw.Write(State);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::InventoryItemUpdate:\n" +
                   $"  {nameof(Id)} = {Id}\n" +
                   $"  {nameof(Licensee_label)} = {Licensee_label}\n" +
                   $"  {nameof(Licensor_label)} = {Licensor_label}\n" +
                   $"  {nameof(Compat_version)} = {Compat_version}\n" +
                   $"  {nameof(Licensor_pid)} = {Licensor_pid}\n" +
                   $"  {nameof(CreationTime)} = {CreationTime}\n" +
                   $"  {nameof(ModificationTime)} = {ModificationTime}\n" +
                   $"  {nameof(Origin)} = {Origin}\n" +
                   $"  {nameof(OriginReference)} = {OriginReference}\n" +
                   $"  {nameof(Revisions)} = [{string.Join(",\r\n", Revisions)}]\n" +
                   $"  {nameof(Offset)} = {Offset}\n" +
                   $"  {nameof(State)} = {State}\n";
        }
    }

}
