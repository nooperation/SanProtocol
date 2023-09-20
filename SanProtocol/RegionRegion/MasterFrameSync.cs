using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.RegionRegion
{
    public class MasterFrameSync : IPacket
    {
        public uint MessageId => Messages.RegionRegionMessages.MasterFrameSync;

        public ulong MasterFrame { get; set; }

        public MasterFrameSync(ulong masterFrame)
        {
            this.MasterFrame = masterFrame;
        }

        public MasterFrameSync(BinaryReader br)
        {
            MasterFrame = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(MasterFrame);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"RegionRegion::MasterFrameSync:\n" +
                   $"  {nameof(MasterFrame)} = {MasterFrame}\n";
        }
    }
}
