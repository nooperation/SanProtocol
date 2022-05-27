﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class RequestDeleteLatestSpawn : IPacket
    {
        public uint MessageId => Messages.AgentController.RequestDeleteLatestSpawn;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }

        public RequestDeleteLatestSpawn(ulong frame, uint agentControllerId)
        {
            this.Frame = frame;
            this.AgentControllerId = agentControllerId;
        }

        public RequestDeleteLatestSpawn(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(AgentControllerId);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::RequestDeleteLatestSpawn:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n";
        }
    }
}