using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class TeleportToUri : IPacket
    {
        public uint MessageId => Messages.ClientRegion.TeleportToUri;

        public string SansarUri { get; set; }

        public TeleportToUri(string sansarUri)
        {
            SansarUri = sansarUri;
        }

        public TeleportToUri(BinaryReader br)
        {
            SansarUri = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(SansarUri);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::TeleportToUri:\n" +
                   $"  {nameof(SansarUri)} = {SansarUri}\n";
        }
    }

}
