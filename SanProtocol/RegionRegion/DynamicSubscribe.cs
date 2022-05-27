﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.RegionRegion
{
    public class DynamicSubscribe : IPacket
    {
        public uint MessageId => Messages.RegionRegion.DynamicSubscribe;


        public DynamicSubscribe()
        {
        }

        public DynamicSubscribe(BinaryReader br)
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
            return $"RegionRegion::DynamicSubscribe";
        }
    }
}