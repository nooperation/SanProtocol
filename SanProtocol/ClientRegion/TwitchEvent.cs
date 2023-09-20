using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class TwitchEvent : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.TwitchEvent;

        public uint EventType { get; set; }
        public float Intensity { get; set; }

        public TwitchEvent(uint eventType, float intensity)
        {
            EventType = eventType;
            Intensity = intensity;
        }

        public TwitchEvent(BinaryReader br)
        {
            EventType = br.ReadUInt32();
            Intensity = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(EventType);
                    bw.Write(Intensity);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::TwitchEvent:\n" +
                   $"  {nameof(EventType)} = {EventType}\n" +
                   $"  {nameof(Intensity)} = {Intensity}\n";
        }
    }

}
