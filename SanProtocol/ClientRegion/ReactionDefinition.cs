using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ReactionDefinition : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ReactionDefinition;

        public string ReactionType { get; set; }
        public string DisplayText { get; set; }
        public SanUUID ThumbnailId { get; set; }

        public ReactionDefinition(string reactionType, string displayText, SanUUID thumbnailId)
        {
            ReactionType = reactionType;
            DisplayText = displayText;
            ThumbnailId = thumbnailId;
        }

        public ReactionDefinition(BinaryReader br)
        {
            ReactionType = br.ReadSanString();
            DisplayText = br.ReadSanString();
            ThumbnailId = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.WriteSanString(ReactionType);
                    bw.WriteSanString(DisplayText);
                    bw.Write(ThumbnailId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ReactionDefinition:\n" +
                   $"  {nameof(ReactionType)} = {ReactionType}\n" +
                   $"  {nameof(DisplayText)} = {DisplayText}\n" +
                   $"  {nameof(ThumbnailId)} = {ThumbnailId}\n";
        }
    }
}
