﻿namespace SanProtocol.ClientRegion
{
    public class TwitchEventSubscription : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.TwitchEventSubscription;

        public uint EventMask { get; set; }

        public TwitchEventSubscription(uint eventMask)
        {
            EventMask = eventMask;
        }

        public TwitchEventSubscription(BinaryReader br)
        {
            EventMask = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(EventMask);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::TwitchEventSubscription:\n" +
                   $"  {nameof(EventMask)} = {EventMask}\n";
        }
    }

}
