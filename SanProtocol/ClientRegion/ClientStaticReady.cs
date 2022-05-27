﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class ClientStaticReady : IPacket
    {
        public uint MessageId => Messages.ClientRegion.ClientStaticReady;

        public byte Ready { get; set; }

        public ClientStaticReady(byte ready)
        {
            Ready = ready;
        }

        public ClientStaticReady(BinaryReader br)
        {
            Ready = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Ready);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientStaticReady:\n" +
                   $"  {nameof(Ready)} = {Ready}\n";
        }
    }

}
