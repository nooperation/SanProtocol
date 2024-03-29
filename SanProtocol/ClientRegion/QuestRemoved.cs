﻿namespace SanProtocol.ClientRegion
{
    public class QuestRemoved : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.QuestRemoved;

        public SanUUID QuestId { get; set; }

        public QuestRemoved(SanUUID questId)
        {
            QuestId = questId;
        }

        public QuestRemoved(BinaryReader br)
        {
            QuestId = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(QuestId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::QuestRemoved:\n" +
                   $"  {nameof(QuestId)} = {QuestId}\n";
        }
    }

}
