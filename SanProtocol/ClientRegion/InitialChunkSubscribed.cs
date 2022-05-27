﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class InitialChunkSubscribed : IPacket
    {
        public uint MessageId => Messages.ClientRegion.InitialChunkSubscribed;

        public byte Unused { get; set; }

        public InitialChunkSubscribed(byte unused)
        {
            Unused = unused;
        }

        public InitialChunkSubscribed(BinaryReader br)
        {
            Unused = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Unused);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::InitialChunkSubscribed:\n" +
                   $"  {nameof(Unused)} = {Unused}\n";
        }
    }

}