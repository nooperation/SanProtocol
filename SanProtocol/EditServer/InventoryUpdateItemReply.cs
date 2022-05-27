﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class InventoryUpdateItemReply : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryUpdateItemReply;

        public SanUUID RequestId { get; set; }
        public SanUUID ItemId { get; set; }
        public string AssetId { get; set; }
        public uint RequestResult { get; set; }
        public string StatusCode { get; set; }
        public string ErrorMsg { get; set; }

        public InventoryUpdateItemReply(SanUUID requestId, SanUUID itemId, string assetId, uint requestResult, string statusCode, string errorMsg)
        {
            this.RequestId = requestId;
            this.ItemId = itemId;
            this.AssetId = assetId;
            this.RequestResult = requestResult;
            this.StatusCode = statusCode;
            this.ErrorMsg = errorMsg;
        }

        public InventoryUpdateItemReply(BinaryReader br)
        {
            RequestId = br.ReadSanUUID();
            ItemId = br.ReadSanUUID();
            AssetId = br.ReadSanString();
            RequestResult = br.ReadUInt32();
            StatusCode = br.ReadSanString();
            ErrorMsg = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(RequestId);
                    bw.Write(ItemId);
                    bw.WriteSanString(AssetId);
                    bw.Write(RequestResult);
                    bw.WriteSanString(StatusCode);
                    bw.WriteSanString(ErrorMsg);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryUpdateItemReply:\n" +
                   $"  {nameof(RequestId)} = {RequestId}\n" +
                   $"  {nameof(ItemId)} = {ItemId}\n" +
                   $"  {nameof(AssetId)} = {AssetId}\n" +
                   $"  {nameof(RequestResult)} = {RequestResult}\n" +
                   $"  {nameof(StatusCode)} = {StatusCode}\n" +
                   $"  {nameof(ErrorMsg)} = {ErrorMsg}\n";
        }
    }
}