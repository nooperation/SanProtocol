using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class InventoryItemUploadReply : IPacket
    {
        public uint MessageId => Messages.EditServer.InventoryItemUploadReply;

        public SanUUID ProductId { get; set; }
        public string Status { get; set; }
        public string ListingUrl { get; set; }
        public string EditUrl { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public InventoryItemUploadReply(SanUUID productId, string status, string listingUrl, string editUrl, string errorCode, string errorMessage)
        {
            this.ProductId = productId;
            this.Status = status;
            this.ListingUrl = listingUrl;
            this.EditUrl = editUrl;
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }

        public InventoryItemUploadReply(BinaryReader br)
        {
            ProductId = br.ReadSanUUID();
            Status = br.ReadSanString();
            ListingUrl = br.ReadSanString();
            EditUrl = br.ReadSanString();
            ErrorCode = br.ReadSanString();
            ErrorMessage = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ProductId);
                    bw.WriteSanString(Status);
                    bw.WriteSanString(ListingUrl);
                    bw.WriteSanString(EditUrl);
                    bw.WriteSanString(ErrorCode);
                    bw.WriteSanString(ErrorMessage);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::InventoryItemUploadReply:\n" +
                   $"  {nameof(ProductId)} = {ProductId}\n" +
                   $"  {nameof(Status)} = {Status}\n" +
                   $"  {nameof(ListingUrl)} = {ListingUrl}\n" +
                   $"  {nameof(EditUrl)} = {EditUrl}\n" +
                   $"  {nameof(ErrorCode)} = {ErrorCode}\n" +
                   $"  {nameof(ErrorMessage)} = {ErrorMessage}\n";
        }
    }
}
