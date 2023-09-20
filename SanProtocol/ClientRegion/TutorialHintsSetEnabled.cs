using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class TutorialHintsSetEnabled : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.TutorialHintsSetEnabled;

        public byte Enabled { get; set; }

        public TutorialHintsSetEnabled(byte enabled)
        {
            Enabled = enabled;
        }

        public TutorialHintsSetEnabled(BinaryReader br)
        {
            Enabled = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Enabled);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::TutorialHintsSetEnabled:\n" +
                   $"  {nameof(Enabled)} = {Enabled}\n";
        }
    }

}
