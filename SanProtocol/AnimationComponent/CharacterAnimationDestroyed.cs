using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AnimationComponent
{
    public class CharacterAnimationDestroyed : IPacket
    {
        public uint MessageId => Messages.AnimationComponent.CharacterAnimationDestroyed;

        public ulong ComponentId { get; set; }

        public CharacterAnimationDestroyed(ulong componentId)
        {
            this.ComponentId = componentId;
        }

        public CharacterAnimationDestroyed(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ComponentId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::CharacterAnimationDestroyed:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n";
        }
    }
}
