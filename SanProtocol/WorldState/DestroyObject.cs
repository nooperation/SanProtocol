namespace SanProtocol.WorldState
{
    public class DestroyObject : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.DestroyObject;

        public uint ObjectId { get; set; }

        public DestroyObject(uint objectId)
        {
            ObjectId = objectId;
        }

        public DestroyObject(BinaryReader br)
        {
            ObjectId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ObjectId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::DestroyObject:\n" +
                   $"  {nameof(ObjectId)} = {ObjectId}\n";
        }
    }
}
