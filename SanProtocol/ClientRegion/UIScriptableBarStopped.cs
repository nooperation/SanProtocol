namespace SanProtocol.ClientRegion
{
    public class UIScriptableBarStopped : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.UIScriptableBarStopped;

        public uint BarId { get; set; }
        public ulong ScriptEventId { get; set; }
        public byte Completed { get; set; }

        public UIScriptableBarStopped(uint barId, ulong scriptEventId, byte completed)
        {
            BarId = barId;
            ScriptEventId = scriptEventId;
            Completed = completed;
        }

        public UIScriptableBarStopped(BinaryReader br)
        {
            BarId = br.ReadUInt32();
            ScriptEventId = br.ReadUInt64();
            Completed = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(BarId);
                    bw.Write(ScriptEventId);
                    bw.Write(Completed);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UIScriptableBarStopped:\n" +
                   $"  {nameof(BarId)} = {BarId}\n" +
                   $"  {nameof(ScriptEventId)} = {ScriptEventId}\n" +
                   $"  {nameof(Completed)} = {Completed}\n";
        }
    }

}
