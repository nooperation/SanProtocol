using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class LocalSetMuteAll : IPacket
    {
        public uint MessageId => Messages.ClientVoice.LocalSetMuteAll;

        public byte MuteAll { get; set; }

        public LocalSetMuteAll(byte muteAll)
        {
            MuteAll = muteAll;
        }

        public LocalSetMuteAll(BinaryReader br)
        {
            MuteAll = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(MuteAll);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalSetMuteAll:\n" +
                   $"  {nameof(MuteAll)} = {MuteAll}\n";
        }
    }

}
