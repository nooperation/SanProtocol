﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    // REMOVED 40.11.0.1810696  (2020-08-13)
    public class CreateSpeechGraphicsPlayer : IPacket
    {
        public uint MessageId => Messages.AgentController.CreateSpeechGraphicsPlayer;

        public uint AgentControllerId { get; set; }
        public byte[] SinkConfigData { get; set; }

        public CreateSpeechGraphicsPlayer(uint agentControllerId, byte[] sinkConfigData)
        {
            this.AgentControllerId = agentControllerId;
            this.SinkConfigData = sinkConfigData;
        }

        public CreateSpeechGraphicsPlayer(BinaryReader br)
        {
            AgentControllerId = br.ReadUInt32();
            var sinkConfigDataLength = br.ReadInt32();
            SinkConfigData = br.ReadBytes(sinkConfigDataLength);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AgentControllerId);
                    bw.Write(SinkConfigData.Length);
                    bw.Write(SinkConfigData);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::CreateSpeechGraphicsPlayer:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(SinkConfigData)} = {SinkConfigData}\n";
        }
    }
}
