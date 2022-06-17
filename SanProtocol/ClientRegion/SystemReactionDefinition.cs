using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class SystemReactionDefinition : IPacket
    {
        public uint MessageId => Messages.ClientRegion.SystemReactionDefinition;

        public string ReactionType { get; set; }
        public string DisplayText { get; set; }
        public string ThumbnailPath { get; set; }

        public SystemReactionDefinition(string reactionType, string displayText, string thumbnailId)
        {
            ReactionType = reactionType;
            DisplayText = displayText;
            ThumbnailPath = thumbnailId;
        }

        public SystemReactionDefinition(BinaryReader br)
        {
            ReactionType = br.ReadSanString();
            DisplayText = br.ReadSanString();
            ThumbnailPath = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.WriteSanString(ReactionType);
                    bw.WriteSanString(DisplayText);
                    bw.WriteSanString(ThumbnailPath);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::SystemReactionDefinition:\n" +
                   $"  {nameof(ReactionType)} = {ReactionType}\n" +
                   $"  {nameof(DisplayText)} = {DisplayText}\n" +
                   $"  {nameof(ThumbnailPath)} = {ThumbnailPath}\n";
        }
    }
}
