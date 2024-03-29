﻿namespace SanProtocol.ClientVoice
{
    public class LocalAudioMute : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.LocalAudioMute;

        public uint AgentControllerId { get; set; }
        public byte ShouldMute { get; set; }

        public LocalAudioMute(uint agentControllerId, byte shouldMute)
        {
            AgentControllerId = agentControllerId;
            ShouldMute = shouldMute;
        }

        public LocalAudioMute(BinaryReader br)
        {
            AgentControllerId = br.ReadUInt32();
            ShouldMute = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AgentControllerId);
                    bw.Write(ShouldMute);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalAudioMute:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ShouldMute)} = {ShouldMute}\n";
        }
    }

}
