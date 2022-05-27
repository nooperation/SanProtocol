using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Audio
{
    public class SetMediaSource : IPacket
    {
        public uint MessageId => Messages.Audio.SetMediaSource;

        public string Url { get; set; }
        public uint MediaWidth { get; set; }
        public uint MediaHeight { get; set; }
        public byte Rebroadcast { get; set; }

        public SetMediaSource(string url, uint mediaWidth, uint mediaHeight, byte rebroadcast)
        {
            this.Url = url;
            this.MediaWidth = mediaWidth;
            this.MediaHeight = mediaHeight;
            this.Rebroadcast = rebroadcast;
        }

        public SetMediaSource(BinaryReader br)
        {
            Url = br.ReadSanString();
            MediaWidth = br.ReadUInt32();
            MediaHeight = br.ReadUInt32();
            Rebroadcast = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Url);
                    bw.Write(MediaWidth);
                    bw.Write(MediaHeight);
                    bw.Write(Rebroadcast);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::SetMediaSource:\n" +
                   $"  {nameof(Url)} = {Url}\n" +
                   $"  {nameof(MediaWidth)} = {MediaWidth}\n" +
                   $"  {nameof(MediaHeight)} = {MediaHeight}\n" +
                   $"  {nameof(Rebroadcast)} = {Rebroadcast}\n";
        }
    }

}
