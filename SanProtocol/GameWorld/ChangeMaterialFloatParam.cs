﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.GameWorld
{
    public class ChangeMaterialFloatParam : IPacket
    {
        public uint MessageId => Messages.GameWorld.ChangeMaterialFloatParam;

        public byte Parameter { get; set; }
        public float Start { get; set; }
        public float End { get; set; }

        public ChangeMaterialFloatParam(byte parameter, float start, float end)
        {
            this.Parameter = parameter;
            this.Start = start;
            this.End = end;
        }

        public ChangeMaterialFloatParam(BinaryReader br)
        {
            Parameter = br.ReadByte();
            Start = br.ReadSingle();
            End = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Parameter);
                    bw.Write(Start);
                    bw.Write(End);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::ChangeMaterialFloatParam:\n" +
                   $"  {nameof(Parameter)} = {Parameter}\n" +
                   $"  {nameof(Start)} = {Start}\n" +
                   $"  {nameof(End)} = {End}\n";
        }
    }
}
