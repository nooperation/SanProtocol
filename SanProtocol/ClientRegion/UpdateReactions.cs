using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class UpdateReactions : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.UpdateReactions;

        public List<ReactionDefinition> ReactionDefinitions { get; set; }
        public List<SystemReactionDefinition> SystemReactionDefinitions { get; set; }

        public UpdateReactions(List<ReactionDefinition> reactionDefinitions, List<SystemReactionDefinition> systemReactionDefinitions)
        {
            ReactionDefinitions = reactionDefinitions;
            SystemReactionDefinitions = systemReactionDefinitions;
        }

        public UpdateReactions(BinaryReader br)
        {
            var reactionsLength = br.ReadInt32();
            ReactionDefinitions = new List<ReactionDefinition>(reactionsLength);
            for (int i = 0; i < reactionsLength; i++)
            {
                var reaction = new ReactionDefinition(br);
                ReactionDefinitions.Add(reaction);
            }

            var systemReactionsLength = br.ReadInt32();
            SystemReactionDefinitions = new List<SystemReactionDefinition>(systemReactionsLength);
            for (int i = 0; i < systemReactionsLength; i++)
            {
                var reaction = new SystemReactionDefinition(br);
                SystemReactionDefinitions.Add(reaction);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write((int)ReactionDefinitions.Count);
                    foreach (var reaction in ReactionDefinitions)
                    {
                        var reactionBytes = reaction.GetBytes().Skip(4).ToArray();
                        bw.Write(reactionBytes);
                    }

                    bw.Write((int)SystemReactionDefinitions.Count);
                    foreach (var reaction in SystemReactionDefinitions)
                    {
                        var reactionBytes = reaction.GetBytes().Skip(4).ToArray();
                        bw.Write(reactionBytes);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UpdateReactions:\n" +
                   $"  {nameof(ReactionDefinitions)} = [{ReactionDefinitions.Count}]\n" +
                   $"  {nameof(SystemReactionDefinitions)} = [{SystemReactionDefinitions.Count}]\n";
        }
    }
}
