using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientKafka
{
    public class InventoryLoaded : IPacket
    {
        public uint MessageId => Messages.ClientKafka.InventoryLoaded;

        public ulong Offset { get; set; }

        public InventoryLoaded(ulong offset)
        {
            Offset = offset;
        }

        public InventoryLoaded(BinaryReader br)
        {
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::InventoryLoaded:\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}
