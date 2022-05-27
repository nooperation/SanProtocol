using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ShowWorldDetail : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ShowWorldDetail;

        public string SansarUri { get; set; }
        public byte Show { get; set; }

        public ShowWorldDetail(string sansarUri, byte show)
        {
            SansarUri = sansarUri;
            Show = show;
        }

        public ShowWorldDetail(BinaryReader br)
        {
            SansarUri = br.ReadSanString();
            Show = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(SansarUri);
                    bw.Write(Show);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ShowWorldDetail:\n" +
                   $"  {nameof(SansarUri)} = {SansarUri}\n" +
                   $"  {nameof(Show)} = {Show}\n";
        }
    }

}
