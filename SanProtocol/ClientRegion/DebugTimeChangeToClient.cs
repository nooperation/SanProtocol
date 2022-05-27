using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class DebugTimeChangeToClient : IPacket
    {
        public uint MessageId => Messages.ClientRegion.DebugTimeChangeToClient;

        public uint RequestId { get; set; }
        public float ClientDeltaTimeForced { get; set; }
        public float ClientDeltaTimeScale { get; set; }
        public byte RequestAccepted { get; set; }
        public string ErrorMessage { get; set; }

        public DebugTimeChangeToClient(uint requestId, float clientDeltaTimeForced, float clientDeltaTimeScale, byte requestAccepted, string errorMessage)
        {
            RequestId = requestId;
            ClientDeltaTimeForced = clientDeltaTimeForced;
            ClientDeltaTimeScale = clientDeltaTimeScale;
            RequestAccepted = requestAccepted;
            ErrorMessage = errorMessage;
        }

        public DebugTimeChangeToClient(BinaryReader br)
        {
            RequestId = br.ReadUInt32();
            ClientDeltaTimeForced = br.ReadSingle();
            ClientDeltaTimeScale = br.ReadSingle();
            RequestAccepted = br.ReadByte();
            ErrorMessage = br.ReadSanString();
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
                    bw.Write(RequestAccepted);
                    bw.WriteSanString(ErrorMessage);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::DebugTimeChangeToClient:\n" +
                   $"  {nameof(RequestId)} = {RequestId}\n" +
                   $"  {nameof(ClientDeltaTimeForced)} = {ClientDeltaTimeForced}\n" +
                   $"  {nameof(ClientDeltaTimeScale)} = {ClientDeltaTimeScale}\n" +
                   $"  {nameof(RequestAccepted)} = {RequestAccepted}\n" +
                   $"  {nameof(ErrorMessage)} = {ErrorMessage}\n";
        }
    }

}
