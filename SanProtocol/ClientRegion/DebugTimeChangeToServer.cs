using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class DebugTimeChangeToServer : IPacket
    {
        public uint MessageId => Messages.ClientRegion.DebugTimeChangeToServer;

        public uint RequestId { get; set; }
        public float ClientDeltaTimeForced { get; set; }
        public float ClientDeltaTimeScale { get; set; }

        public DebugTimeChangeToServer(uint requestId, float clientDeltaTimeForced, float clientDeltaTimeScale)
        {
            RequestId = requestId;
            ClientDeltaTimeForced = clientDeltaTimeForced;
            ClientDeltaTimeScale = clientDeltaTimeScale;
        }

        public DebugTimeChangeToServer(BinaryReader br)
        {
            RequestId = br.ReadUInt32();
            ClientDeltaTimeForced = br.ReadSingle();
            ClientDeltaTimeScale = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(RequestId);
                    bw.Write(ClientDeltaTimeForced);
                    bw.Write(ClientDeltaTimeScale);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::DebugTimeChangeToServer:\n" +
                   $"  {nameof(RequestId)} = {RequestId}\n" +
                   $"  {nameof(ClientDeltaTimeForced)} = {ClientDeltaTimeForced}\n" +
                   $"  {nameof(ClientDeltaTimeScale)} = {ClientDeltaTimeScale}\n";
        }
    }

}
