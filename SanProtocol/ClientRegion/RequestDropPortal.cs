using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class RequestDropPortal : IPacket
    {
        public uint MessageId => Messages.ClientRegion.RequestDropPortal;

        public string SansarUri { get; set; }
        public string SansarUriDescription { get; set; }

        public RequestDropPortal(string sansarUri, string sansarUriDescription)
        {
            SansarUri = sansarUri;
            SansarUriDescription = sansarUriDescription;
        }

        public RequestDropPortal(BinaryReader br)
        {
            SansarUri = br.ReadSanString();
            SansarUriDescription = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(SansarUri);
                    bw.WriteSanString(SansarUriDescription);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::RequestDropPortal:\n" +
                   $"  {nameof(SansarUri)} = {SansarUri}\n" +
                   $"  {nameof(SansarUriDescription)} = {SansarUriDescription}\n";
        }
    }

}
