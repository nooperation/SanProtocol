using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class OpenQuestCharacterDialog : IPacket
    {
        public uint MessageId => Messages.ClientRegion.OpenQuestCharacterDialog;

        public SanUUID CharacterId { get; set; }

        public OpenQuestCharacterDialog(SanUUID characterId)
        {
            CharacterId = characterId;
        }

        public OpenQuestCharacterDialog(BinaryReader br)
        {
            CharacterId = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(CharacterId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::OpenQuestCharacterDialog:\n" +
                   $"  {nameof(CharacterId)} = {CharacterId}\n";
        }
    }

}
