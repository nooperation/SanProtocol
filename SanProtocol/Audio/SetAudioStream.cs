using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Audio
{
    public class SetAudioStream : IPacket
    {
        public uint MessageId => Messages.AudioMessages.SetAudioStream;

        public string Url { get; set; }
        public byte Rebroadcast { get; set; }

        public SetAudioStream(string url, byte rebroadcast)
        {
            this.Url = url;
            this.Rebroadcast = rebroadcast;
        }

        public SetAudioStream(BinaryReader br)
        {
            Url = br.ReadSanString();
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
                    bw.Write(Rebroadcast);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Audio::SetAudioStream:\n" +
                   $"  {nameof(Url)} = {Url}\n" +
                   $"  {nameof(Rebroadcast)} = {Rebroadcast}\n";
        }
    }
}
