namespace SanProtocol.EditServer
{
    public class SaveWorkspaceSelectionToInventoryReply : IPacket
    {
        public uint MessageId => Messages.EditServerMessages.SaveWorkspaceSelectionToInventoryReply;

        public string ItemName { get; set; }
        public SanUUID ItemId { get; set; }
        public uint TriggeringState { get; set; }
        public uint RequestResult { get; set; }
        public uint StatusCode { get; set; }

        public SaveWorkspaceSelectionToInventoryReply(string itemName, SanUUID itemId, uint triggeringState, uint requestResult, uint statusCode)
        {
            ItemName = itemName;
            ItemId = itemId;
            TriggeringState = triggeringState;
            RequestResult = requestResult;
            StatusCode = statusCode;
        }

        public SaveWorkspaceSelectionToInventoryReply(BinaryReader br)
        {
            ItemName = br.ReadSanString();
            ItemId = br.ReadSanUUID();
            TriggeringState = br.ReadUInt32();
            RequestResult = br.ReadUInt32();
            StatusCode = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(ItemName);
                    bw.Write(ItemId);
                    bw.Write(TriggeringState);
                    bw.Write(RequestResult);
                    bw.Write(StatusCode);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::SaveWorkspaceSelectionToInventoryReply:\n" +
                   $"  {nameof(ItemName)} = {ItemName}\n" +
                   $"  {nameof(ItemId)} = {ItemId}\n" +
                   $"  {nameof(TriggeringState)} = {TriggeringState}\n" +
                   $"  {nameof(RequestResult)} = {RequestResult}\n" +
                   $"  {nameof(StatusCode)} = {StatusCode}\n";
        }
    }
}
