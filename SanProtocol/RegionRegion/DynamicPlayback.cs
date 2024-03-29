﻿namespace SanProtocol.RegionRegion
{
    public class DynamicPlayback : IPacket
    {
        public uint MessageId => Messages.RegionRegionMessages.DynamicPlayback;


        public DynamicPlayback()
        {
        }

        public DynamicPlayback(BinaryReader br)
        {
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"RegionRegion::DynamicPlayback";
        }
    }
}
