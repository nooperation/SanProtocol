using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.ClientVoice
{
    public class LocalAudioPosition : IPacket
    {
        public uint MessageId => Messages.ClientVoice.LocalAudioPosition;

        public uint Sequence { get; set; }
        public SanUUID Instance { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public uint AgentControllerId { get; set; }

        public LocalAudioPosition(uint sequence, SanUUID instance, List<float> position, uint agentControllerId)
        {
            this.Sequence = sequence;
            this.Instance = instance;
            this.Position = position;
            this.AgentControllerId = agentControllerId;
        }

        public LocalAudioPosition(BinaryReader br)
        {
            Sequence = br.ReadUInt32();
            Instance = br.ReadSanUUID();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Position.Add(item);
            }
            AgentControllerId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Sequence);
                    bw.Write(Instance);
                    foreach (var item in Position)
                    {
                        bw.Write(item);
                    }
                    bw.Write(AgentControllerId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalAudioPosition:\n" +
                   $"  {nameof(Sequence)} = {Sequence}\n" +
                   $"  {nameof(Instance)} = {Instance}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n";
        }
    }
}
