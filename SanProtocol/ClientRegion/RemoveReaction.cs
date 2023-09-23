namespace SanProtocol.ClientRegion
{
    public class RemoveReaction : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.RemoveReaction;

        public string ReactionType { get; set; }

        public RemoveReaction(string reactionType)
        {
            ReactionType = reactionType;
        }

        public RemoveReaction(BinaryReader br)
        {
            ReactionType = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.WriteSanString(ReactionType);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::RemoveReaction:\n" +
                   $"  {nameof(ReactionType)} = {ReactionType}\n";
        }
    }
}
