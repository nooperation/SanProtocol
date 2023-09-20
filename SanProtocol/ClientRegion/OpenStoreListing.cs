using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class OpenStoreListing : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.OpenStoreListing;

        public SanUUID ListingId { get; set; }

        public OpenStoreListing(SanUUID listingId)
        {
            ListingId = listingId;
        }

        public OpenStoreListing(BinaryReader br)
        {
            ListingId = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ListingId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::OpenStoreListing:\n" +
                   $"  {nameof(ListingId)} = {ListingId}\n";
        }
    }

}
