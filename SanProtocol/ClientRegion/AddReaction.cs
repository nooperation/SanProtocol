using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class AddReaction : IPacket
    {
        public uint MessageId => Messages.ClientRegion.AddReaction;

        public string ReactionType { get; set; }
        public string DisplayText { get; set; }
        public SanUUID ThumbnailId { get; set; }

        public AddReaction(string reactionType, string displayText, SanUUID thumbnailId)
        {
            ReactionType = reactionType;
            DisplayText = displayText;
            ThumbnailId = thumbnailId;
        }

        public AddReaction(BinaryReader br)
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
            return $"ClientRegion::AddReaction:\n" +
                   $"  {nameof(ReactionType)} = {ReactionType}\n" +
                   $"  {nameof(DisplayText)} = {DisplayText}\n" +
                   $"  {nameof(ThumbnailId)} = {ThumbnailId}\n";
        }
    }
}
