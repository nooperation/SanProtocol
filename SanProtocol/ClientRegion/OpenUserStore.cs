﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class OpenUserStore : IPacket
    {
        public uint MessageId => Messages.ClientRegion.OpenUserStore;

        public string CreatorHandle { get; set; }

        public OpenUserStore(string creatorHandle)
        {
            CreatorHandle = creatorHandle;
        }

        public OpenUserStore(BinaryReader br)
        {
            CreatorHandle = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(CreatorHandle);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::OpenUserStore:\n" +
                   $"  {nameof(CreatorHandle)} = {CreatorHandle}\n";
        }
    }

}
