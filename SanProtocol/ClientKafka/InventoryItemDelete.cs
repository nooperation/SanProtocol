﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class InventoryItemDelete : IPacket
    {
        public uint MessageId => Messages.ClientKafka.InventoryItemDelete;

        public string Id { get; set; }
        public ulong Offset { get; set; }

        public InventoryItemDelete(string id, ulong offset)
        {
            Id = id;
            Offset = offset;
        }

        public InventoryItemDelete(BinaryReader br)
        {
            Id = br.ReadSanString();
            Offset = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Id);
                    bw.Write(Offset);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::InventoryItemDelete:\n" +
                   $"  {nameof(Id)} = {Id}\n" +
                   $"  {nameof(Offset)} = {Offset}\n";
        }
    }

}