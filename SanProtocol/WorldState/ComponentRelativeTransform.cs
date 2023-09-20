using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.WorldState
{
    public class ComponentRelativeTransform : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.ComponentRelativeTransform;

        public List<float> RelativePosition { get; set; } = new List<float>();
        public List<float> RelativeRotation { get; set; } = new List<float>();
        public ulong ComponentId { get; set; }

        public ComponentRelativeTransform(List<float> relativePosition, List<float> relativeRotation, ulong componentId)
        {
            this.RelativePosition = relativePosition;
            this.RelativeRotation = relativeRotation;
            this.ComponentId = componentId;
        }

        public ComponentRelativeTransform(BinaryReader br)
        {
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                RelativePosition.Add(item);
            }
            for (var i = 0; i < 4; ++i)
            {
                var item = br.ReadSingle();
                RelativeRotation.Add(item);
            }
            ComponentId = br.ReadUInt64();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    foreach (var item in RelativePosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in RelativeRotation)
                    {
                        bw.Write(item);
                    }
                    bw.Write(ComponentId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::ComponentRelativeTransform:\n" +
                   $"  {nameof(RelativePosition)} = <{String.Join(',', RelativePosition)}>\n" +
                   $"  {nameof(RelativeRotation)} = <{String.Join(',', RelativeRotation)}>\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n";
        }
    }
}
