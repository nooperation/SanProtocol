﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class VoiceNotification : IPacket
    {
        public uint MessageId => Messages.ClientVoice.VoiceNotification;

        public string Notification { get; set; }

        public VoiceNotification(string notification)
        {
            Notification = notification;
        }

        public VoiceNotification(BinaryReader br)
        {
            Notification = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Notification);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::VoiceNotification:\n" +
                   $"  {nameof(Notification)} = {Notification}\n";
        }
    }

}