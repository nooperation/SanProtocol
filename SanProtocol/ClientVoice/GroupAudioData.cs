using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class GroupAudioData : IPacket
    {
        public uint MessageId => Messages.ClientVoice.GroupAudioData;

        public string Group { get; set; }
        public string User { get; set; }

        public GroupAudioData(string group, string user)
        {
            Group = group;
            User = user;
        }

        public GroupAudioData(BinaryReader br)
        {
            Group = br.ReadSanString();
            User = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Group);
                    bw.WriteSanString(User);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::GroupAudioData:\n" +
                   $"  {nameof(Group)} = {Group}\n" +
                   $"  {nameof(User)} = {User}\n";
        }
    }

}
