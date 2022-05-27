﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientKafka
{
    public class LoginReply : IPacket
    {
        public uint MessageId => Messages.ClientKafka.LoginReply;

        public bool Success { get; set; }
        public string Message { get; set; }

        public LoginReply(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public LoginReply(BinaryReader br)
        {
            Success = br.ReadBoolean();
            Message = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Success);
                    bw.WriteSanString(Message);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientKafka::LoginReply:\n" +
                   $"  {nameof(Success)} = {Success}\n" +
                   $"  {nameof(Message)} = {Message}\n";
        }
    }

}