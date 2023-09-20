using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ScriptModalDialogResponse : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ScriptModalDialogResponse;

        public ulong EventId { get; set; }
        public string Response { get; set; }

        public ScriptModalDialogResponse(ulong eventId, string response)
        {
            EventId = eventId;
            Response = response;
        }

        public ScriptModalDialogResponse(BinaryReader br)
        {
            EventId = br.ReadUInt64();
            Response = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(EventId);
                    bw.WriteSanString(Response);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ScriptModalDialogResponse:\n" +
                   $"  {nameof(EventId)} = {EventId}\n" +
                   $"  {nameof(Response)} = {Response}\n";
        }
    }

}
