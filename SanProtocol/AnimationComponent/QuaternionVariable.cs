﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class QuaternionVariable : IPacket
    {
        public uint MessageId => Messages.AnimationComponent.QuaternionVariable;

        public ushort InternalId { get; set; }
        public List<float> Value { get; set; } = new List<float>();

        public QuaternionVariable(ushort internalId, List<float> value)
        {
            this.InternalId = internalId;
            this.Value = value;
        }

        public QuaternionVariable(BinaryReader br)
        {
            InternalId = br.ReadUInt16();
            for (var i = 0; i < 4; ++i)
            {
                var item = br.ReadSingle();
                Value.Add(item);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(InternalId);
                    foreach (var item in Value)
                    {
                        bw.Write(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::QuaternionVariable:\n" +
                   $"  {nameof(InternalId)} = {InternalId}\n" +
                   $"  {nameof(Value)} = <{String.Join(',', Value)}>\n";
        }
    }
}
