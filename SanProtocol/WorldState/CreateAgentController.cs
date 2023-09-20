using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SanProtocol.WorldState
{
    public class CreateAgentController : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.CreateAgentController;

        public uint SessionId { get; set; }
        public uint ClusterId { get; set; }
        public uint AgentControllerId { get; set; }
        public uint CharacterObjectId { get; set; }
        public List<CreateCharacterNode> CharacterNodes { get; set; } = new List<CreateCharacterNode>();
        public ulong Frame { get; set; }
        public SanUUID PersonaId { get; set; }
        public bool IsRemoteAgent { get; set; }

        public CreateAgentController(uint sessionId, uint clusterId, uint agentControllerId, uint characterObjectId, List<CreateCharacterNode> characterNodes, ulong frame, SanUUID personaId, byte isRemoteAgent)
        {
            this.SessionId = sessionId;
            this.ClusterId = clusterId;
            this.AgentControllerId = agentControllerId;
            this.CharacterObjectId = characterObjectId;
            this.CharacterNodes = characterNodes;
            this.Frame = frame;
            this.PersonaId = personaId;
            this.IsRemoteAgent = isRemoteAgent != 0;
        }

        public CreateAgentController(BinaryReader br)
        {
            SessionId = br.ReadUInt32();
            ClusterId = br.ReadUInt32();
            AgentControllerId = br.ReadUInt32();
            CharacterObjectId = br.ReadUInt32();

            var characterNodesLength = br.ReadUInt32();
            for (int i = 0; i < characterNodesLength; i++)
            {
                CharacterNodes.Add(new CreateCharacterNode(br));
            }

            Frame = br.ReadUInt64();
            PersonaId = br.ReadSanUUID();
            IsRemoteAgent = br.ReadByte() != 0;
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(SessionId);
                    bw.Write(ClusterId);
                    bw.Write(AgentControllerId);
                    bw.Write(CharacterObjectId);

                    bw.Write(CharacterNodes.Count);
                    foreach (var item in CharacterNodes)
                    {
                        // Skip the messageId (the first 4 bytes) of GetBytes()...
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(Frame);
                    bw.Write(PersonaId);
                    bw.Write(IsRemoteAgent);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::CreateAgentController:\n" +
                   $"  {nameof(SessionId)} = {SessionId}\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(CharacterObjectId)} = {CharacterObjectId}\n" +
                   $"  {nameof(CharacterNodes)} = {string.Join(",", CharacterNodes)}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(PersonaId)} = {PersonaId}\n" +
                   $"  {nameof(IsRemoteAgent)} = {IsRemoteAgent}\n";
        }
    }
}
