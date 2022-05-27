using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class TeleportTo : IPacket
    {
        public uint MessageId => Messages.ClientRegion.TeleportTo;

        public string PersonaHandle { get; set; }
        public string LocationHandle { get; set; }

        public TeleportTo(string personaHandle, string locationHandle)
        {
            PersonaHandle = personaHandle;
            LocationHandle = locationHandle;
        }

        public TeleportTo(BinaryReader br)
        {
            PersonaHandle = br.ReadSanString();
            LocationHandle = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(PersonaHandle);
                    bw.WriteSanString(LocationHandle);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::TeleportTo:\n" +
                   $"  {nameof(PersonaHandle)} = {PersonaHandle}\n" +
                   $"  {nameof(LocationHandle)} = {LocationHandle}\n";
        }
    }

}
