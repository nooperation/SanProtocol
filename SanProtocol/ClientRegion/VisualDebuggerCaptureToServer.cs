using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class VisualDebuggerCaptureToServer : IPacket
    {
        public uint MessageId => Messages.ClientRegion.VisualDebuggerCaptureToServer;

        public string StartTimeFormatted { get; set; }
        public byte BeginCapture { get; set; }
        public List<string> Viewers { get; set; } = new List<string>();

        public VisualDebuggerCaptureToServer(string startTimeFormatted, byte beginCapture, List<string> viewers)
        {
            this.StartTimeFormatted = startTimeFormatted;
            this.BeginCapture = beginCapture;
            this.Viewers = viewers;
        }

        public VisualDebuggerCaptureToServer(BinaryReader br)
        {
            StartTimeFormatted = br.ReadSanString();
            BeginCapture = br.ReadByte();
            var numViewers = br.ReadUInt32();
            for (var i = 0; i < numViewers; ++i)
            {
                var str = br.ReadSanString();
                Viewers.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(StartTimeFormatted);
                    bw.Write(BeginCapture);
                    bw.Write(Viewers.Count);
                    foreach (var item in Viewers)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::VisualDebuggerCaptureToServer:\n" +
                   $"  {nameof(StartTimeFormatted)} = {StartTimeFormatted}\n" +
                   $"  {nameof(BeginCapture)} = {BeginCapture}\n" +
                   $"  {nameof(Viewers)} = {String.Join(',', Viewers)}\n";
        }
    }
}
