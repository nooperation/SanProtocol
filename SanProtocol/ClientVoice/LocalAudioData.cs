using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientVoice
{
    public class LocalAudioData : IPacket
    {
        public uint MessageId => Messages.ClientVoiceMessages.LocalAudioData;

        public SanUUID Instance { get; set; }
        public uint AgentControllerId { get; set; }
        public AudioData Data { get; set; }
        public SpeechGraphicsData SpeechGraphicsData { get; set; }
        public byte Broadcast { get; set; }

        public LocalAudioData(SanUUID instance, uint agentControllerId, AudioData data, SpeechGraphicsData speechGraphicsData, byte broadcast)
        {
            Instance = instance;
            AgentControllerId = agentControllerId;
            Data = data;
            SpeechGraphicsData = speechGraphicsData;
            Broadcast = broadcast;
        }

        public LocalAudioData(BinaryReader br)
        {
            Instance = br.ReadSanUUID();
            AgentControllerId = br.ReadUInt32();
            Data = new AudioData(br);
            SpeechGraphicsData = new SpeechGraphicsData(br);
            Broadcast = br.ReadByte();
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
                    bw.Write(Data.GetBytes().Skip(4).ToArray());
                    bw.Write(SpeechGraphicsData.GetBytes().Skip(4).ToArray());
                    bw.Write(Broadcast);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientVoice::LocalAudioData:\n" +
                   $"  {nameof(Instance)} = {Instance}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Data)} = {Data}\n" +
                   $"  {nameof(SpeechGraphicsData)} = {SpeechGraphicsData}\n" +
                   $"  {nameof(Broadcast)} = {Broadcast}\n";
        }
    }

}
