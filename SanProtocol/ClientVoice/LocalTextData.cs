using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class LocalTextData : IPacket
    {
        public uint MessageId => Messages.ClientVoice.LocalTextData;

        public SanUUID Instance { get; set; }
        public uint AgentControllerId { get; set; }
        public string Data { get; set; }

        public LocalTextData(SanUUID instance, uint agentControllerId, string data)
        {
            Instance = instance;
            AgentControllerId = agentControllerId;
            Data = data;
        }

        public LocalTextData(BinaryReader br)
        {
            Instance = br.ReadSanUUID();
            AgentControllerId = br.ReadUInt32();
            Data = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Instance);
                    bw.Write(AgentControllerId);
                    bw.WriteSanString(Data);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalTextData:\n" +
                   $"  {nameof(Instance)} = {Instance}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Data)} = {Data}\n";
        }
    }

}
