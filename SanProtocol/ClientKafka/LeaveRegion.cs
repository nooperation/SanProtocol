using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class LeaveRegion : IPacket
    {
        public uint MessageId => Messages.ClientKafka.LeaveRegion;

        public string RegionAddress { get; set; }

        public LeaveRegion(string regionAddress)
        {
            RegionAddress = regionAddress;
        }

        public LeaveRegion(BinaryReader br)
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
            return $"ClientKafka::LeaveRegion:\n" +
                   $"  {nameof(RegionAddress)} = {RegionAddress}\n";
        }
    }

}
