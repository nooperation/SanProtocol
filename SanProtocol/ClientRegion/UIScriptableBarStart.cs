using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientRegion
{
    public class UIScriptableBarStart : IPacket
    {
        public uint MessageId => Messages.ClientRegion.UIScriptableBarStart;

        public uint BarId { get; set; }
        public ulong ScriptEventId { get; set; }
        public string Label { get; set; }
        public float Duration { get; set; }
        public List<float> Color { get; set; } = new List<float>();
        public float StartPct { get; set; }
        public float EndPct { get; set; }
        public byte Options { get; set; }
        public byte Start { get; set; }

        public UIScriptableBarStart(uint barId, ulong scriptEventId, string label, float duration, List<float> color, float startPct, float endPct, byte options, byte start)
        {
            this.BarId = barId;
            this.ScriptEventId = scriptEventId;
            this.Label = label;
            this.Duration = duration;
            this.Color = color;
            this.StartPct = startPct;
            this.EndPct = endPct;
            this.Options = options;
            this.Start = start;
        }

        public UIScriptableBarStart(BinaryReader br)
        {
            BarId = br.ReadUInt32();
            ScriptEventId = br.ReadUInt64();
            Label = br.ReadSanString();
            Duration = br.ReadSingle();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Color.Add(item);
            }
            StartPct = br.ReadSingle();
            EndPct = br.ReadSingle();
            Options = br.ReadByte();
            Start = br.ReadByte();
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
                    bw.WriteSanString(Label);
                    bw.Write(Duration);
                    foreach (var item in Color)
                    {
                        bw.Write(item);
                    }
                    bw.Write(StartPct);
                    bw.Write(EndPct);
                    bw.Write(Options);
                    bw.Write(Start);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UIScriptableBarStart:\n" +
                   $"  {nameof(BarId)} = {BarId}\n" +
                   $"  {nameof(ScriptEventId)} = {ScriptEventId}\n" +
                   $"  {nameof(Label)} = {Label}\n" +
                   $"  {nameof(Duration)} = {Duration}\n" +
                   $"  {nameof(Color)} = <{String.Join(',', Color)}>\n" +
                   $"  {nameof(StartPct)} = {StartPct}\n" +
                   $"  {nameof(EndPct)} = {EndPct}\n" +
                   $"  {nameof(Options)} = {Options}\n" +
                   $"  {nameof(Start)} = {Start}\n";
        }
    }

}
