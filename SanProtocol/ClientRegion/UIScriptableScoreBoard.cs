namespace SanProtocol.ClientRegion
{
    public class UIScriptableScoreBoard : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.UIScriptableScoreBoard;

        public int BoardId { get; set; }
        public string Score0 { get; set; }
        public string Score1 { get; set; }
        public List<float> ColorScoreFg0 { get; set; }
        public List<float> ColorScoreFg1 { get; set; }
        public List<float> ColorScoreBg0 { get; set; }
        public List<float> ColorScoreBg1 { get; set; }
        public List<float> ColorFg { get; set; }
        public List<float> ColorBg { get; set; }

        public UIScriptableScoreBoard(BinaryReader br)
        {
            BoardId = br.ReadInt32();
            Score0 = br.ReadString();
            Score1 = br.ReadString();
            ColorScoreFg0 = new List<float>()
            {
                br.ReadSingle(),
                br.ReadSingle(),
                br.ReadSingle(),
            };
            ColorScoreFg1 = new List<float>()
            {
                br.ReadSingle(),
                br.ReadSingle(),
                br.ReadSingle(),
            };
            ColorScoreBg0 = new List<float>()
            {
                br.ReadSingle(),
                br.ReadSingle(),
                br.ReadSingle(),
            };
            ColorScoreBg1 = new List<float>()
            {
                br.ReadSingle(),
                br.ReadSingle(),
                br.ReadSingle(),
            };
            ColorFg = new List<float>()
            {
                br.ReadSingle(),
                br.ReadSingle(),
                br.ReadSingle(),
            };
            ColorBg = new List<float>()
            {
                br.ReadSingle(),
                br.ReadSingle(),
                br.ReadSingle(),
            };
        }

        public byte[] GetBytes()
        {
            using MemoryStream ms = new();
            using (BinaryWriter bw = new(ms))
            {
                bw.Write(BoardId);

                bw.WriteSanString(Score0);
                bw.WriteSanString(Score1);

                bw.Write(ColorScoreFg0[0]);
                bw.Write(ColorScoreFg0[1]);
                bw.Write(ColorScoreFg0[2]);

                bw.Write(ColorScoreFg1[0]);
                bw.Write(ColorScoreFg1[1]);
                bw.Write(ColorScoreFg1[2]);

                bw.Write(ColorScoreBg0[0]);
                bw.Write(ColorScoreBg0[1]);
                bw.Write(ColorScoreBg0[2]);

                bw.Write(ColorScoreBg1[0]);
                bw.Write(ColorScoreBg1[1]);
                bw.Write(ColorScoreBg1[2]);

                bw.Write(ColorFg[0]);
                bw.Write(ColorFg[1]);
                bw.Write(ColorFg[2]);

                bw.Write(ColorBg[0]);
                bw.Write(ColorBg[1]);
                bw.Write(ColorBg[2]);
            }
            return ms.ToArray();
        }

        public override string ToString()
        {
            return $"ClientRegion::UIScriptableScoreBoard:\n" +
                   $"  {nameof(BoardId)} = {BoardId}\n" +
                   $"  {nameof(Score0)} = {Score0}\n" +
                   $"  {nameof(Score1)} = {Score1}\n" +
                   $"  {nameof(ColorScoreFg0)} = <{string.Join(',', ColorScoreFg0)}>\n" +
                   $"  {nameof(ColorScoreFg1)} = <{string.Join(',', ColorScoreFg1)}>\n" +
                   $"  {nameof(ColorScoreBg0)} = <{string.Join(',', ColorScoreBg0)}>\n" +
                   $"  {nameof(ColorScoreBg1)} = <{string.Join(',', ColorScoreBg1)}>\n" +
                   $"  {nameof(ColorFg)} = <{string.Join(',', ColorFg)}>\n" +
                   $"  {nameof(ColorBg)} = <{string.Join(',', ColorBg)}>\n";
        }
    }
}
