using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class UIHintTextUpdate : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.UIHintTextUpdate;

        public string Text { get; set; }

        public UIHintTextUpdate(string text)
        {
            Text = text;
        }

        public UIHintTextUpdate(BinaryReader br)
        {
            Text = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Text);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UIHintTextUpdate:\n" +
                   $"  {nameof(Text)} = {Text}\n";
        }
    }

}
