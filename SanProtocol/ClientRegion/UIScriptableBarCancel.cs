using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class UIScriptableBarCancel : IPacket
    {
        public uint MessageId => Messages.ClientRegion.UIScriptableBarCancel;

        public uint BarId { get; set; }

        public UIScriptableBarCancel(uint barId)
        {
            BarId = barId;
        }

        public UIScriptableBarCancel(BinaryReader br)
        {
            BarId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(BarId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UIScriptableBarCancel:\n" +
                   $"  {nameof(BarId)} = {BarId}\n";
        }
    }

}
