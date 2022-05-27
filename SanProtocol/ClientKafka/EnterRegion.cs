using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class EnterRegion : IPacket
    {
        public uint MessageId => Messages.ClientKafka.EnterRegion;

        public string RegionAddress { get; set; }

        public EnterRegion(string regionAddress)
        {
            RegionAddress = regionAddress;
        }

        public EnterRegion(BinaryReader br)
        {
            RegionAddress = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(RegionAddress);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::EnterRegion:\n" +
                   $"  {nameof(RegionAddress)} = {RegionAddress}\n";
        }
    }

}
