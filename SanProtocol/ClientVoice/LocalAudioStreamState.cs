using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientVoice
{
    public class LocalAudioStreamState : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.LocalAudioStreamState;

        public SanUUID Instance { get; set; }
        public uint AgentControllerId { get; set; }
        public byte Broadcast { get; set; }
        public byte Mute { get; set; } // TODO: This is a flag for the inverse. 1 = unmuted?

        public LocalAudioStreamState(SanUUID instance, uint agentControllerId, byte broadcast, byte mute)
        {
            Instance = instance;
            AgentControllerId = agentControllerId;
            Broadcast = broadcast;
            Mute = mute;
        }

        public LocalAudioStreamState(BinaryReader br)
        {
            Instance = br.ReadSanUUID();
            AgentControllerId = br.ReadUInt32();
            Broadcast = br.ReadByte();
            Mute = br.ReadByte();
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
                    bw.Write(Broadcast);
                    bw.Write(Mute);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalAudioStreamState:\n" +
                   $"  {nameof(Instance)} = {Instance}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Broadcast)} = {Broadcast}\n" +
                   $"  {nameof(Mute)} = {Mute}\n";
        }
    }

}
