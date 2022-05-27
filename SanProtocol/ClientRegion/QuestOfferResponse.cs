using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class QuestOfferResponse : IPacket
    {
        public uint MessageId => Messages.ClientRegion.QuestOfferResponse;

        public SanUUID QuestId { get; set; }
        public SanUUID QuestDefinitionId { get; set; }
        public byte Accepted { get; set; }

        public QuestOfferResponse(SanUUID questId, SanUUID questDefinitionId, byte accepted)
        {
            QuestId = questId;
            QuestDefinitionId = questDefinitionId;
            Accepted = accepted;
        }

        public QuestOfferResponse(BinaryReader br)
        {
            QuestId = br.ReadSanUUID();
            QuestDefinitionId = br.ReadSanUUID();
            Accepted = br.ReadByte();
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
                    bw.Write(Accepted);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::QuestOfferResponse:\n" +
                   $"  {nameof(QuestId)} = {QuestId}\n" +
                   $"  {nameof(QuestDefinitionId)} = {QuestDefinitionId}\n" +
                   $"  {nameof(Accepted)} = {Accepted}\n";
        }
    }

}
