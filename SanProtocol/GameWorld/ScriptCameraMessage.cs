﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.GameWorld
{
    public class ScriptCameraMessage : IPacket
    {
        public uint MessageId => Messages.GameWorld.ScriptCameraMessage;

        public ulong Componentid { get; set; }
        public ulong Frame { get; set; }
        public byte ControlMode { get; set; } // 4 bits?

        public ScriptCameraMessage(ulong componentid, ulong frame, byte controlMode)
        {
            this.Componentid = componentid;
            this.Frame = frame;
            this.ControlMode = controlMode;
        }

        public ScriptCameraMessage(BinaryReader br)
        {
            Componentid = br.ReadUInt64();
            Frame = br.ReadUInt64();

            var bitReader = new BitReader(br, 4);
            ControlMode = (byte)bitReader.ReadUnsigned(4);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Componentid);
                    bw.Write(Frame);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteUnsigned(ControlMode, 4);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::ScriptCameraMessage:\n" +
                   $"  {nameof(Componentid)} = {Componentid}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ControlMode)} = {ControlMode}\n";
        }
    }
}
