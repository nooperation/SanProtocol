using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientVoice
{
    public class MasterInstance : IPacket
    {
        public uint MessageId => Messages.ClientVoice.MasterInstance;

        public SanUUID Instance { get; set; }

        public MasterInstance(SanUUID instance)
        {
            Instance = instance;
        }

        public MasterInstance(BinaryReader br)
        {
            Instance = br.ReadSanUUID();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Instance);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::MasterInstance:\n" +
                   $"  {nameof(Instance)} = {Instance}\n";
        }
    }

}
