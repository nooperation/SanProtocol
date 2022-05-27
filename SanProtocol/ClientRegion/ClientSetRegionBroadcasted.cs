using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ClientSetRegionBroadcasted : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientSetRegionBroadcasted;

        public byte Broadcasted { get; set; }

        public ClientSetRegionBroadcasted(byte broadcasted)
        {
            Broadcasted = broadcasted;
        }

        public ClientSetRegionBroadcasted(BinaryReader br)
        {
            Broadcasted = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Broadcasted);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientSetRegionBroadcasted:\n" +
                   $"  {nameof(Broadcasted)} = {Broadcasted}\n";
        }
    }

}
