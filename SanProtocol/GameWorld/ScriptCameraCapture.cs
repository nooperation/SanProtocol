﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.GameWorld
{
    public class ScriptCameraCapture : IPacket
    {
        public uint MessageId => Messages.GameWorld.ScriptCameraMessage;

        public ulong Componentid { get; set; }

        public ScriptCameraCapture(ulong componentid)
        {
            this.Componentid = componentid;
        }

        public ScriptCameraCapture(BinaryReader br)
        {
            Componentid = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Componentid);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::ScriptCameraCapture:\n" +
                   $"  {nameof(Componentid)} = {Componentid}\n";
        }
    }
}
