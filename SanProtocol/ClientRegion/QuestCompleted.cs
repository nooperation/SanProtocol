using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class QuestCompleted : IPacket
    {
        public uint MessageId => Messages.ClientRegion.QuestCompleted;

        public SanUUID QuestId { get; set; }
        public SanUUID QuestDefinitionId { get; set; }
        public uint CompletedState { get; set; }

        public QuestCompleted(SanUUID questId, SanUUID questDefinitionId, uint completedState)
        {
            QuestId = questId;
            QuestDefinitionId = questDefinitionId;
            CompletedState = completedState;
        }

        public QuestCompleted(BinaryReader br)
        {
            QuestId = br.ReadSanUUID();
            QuestDefinitionId = br.ReadSanUUID();
            CompletedState = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(QuestId);
                    bw.Write(QuestDefinitionId);
                    bw.Write(CompletedState);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::QuestCompleted:\n" +
                   $"  {nameof(QuestId)} = {QuestId}\n" +
                   $"  {nameof(QuestDefinitionId)} = {QuestDefinitionId}\n" +
                   $"  {nameof(CompletedState)} = {CompletedState}\n";
        }
    }

}
