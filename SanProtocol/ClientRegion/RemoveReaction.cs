using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class RemoveReaction : IPacket
    {
        public uint MessageId => Messages.ClientRegion.RemoveReaction;

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
