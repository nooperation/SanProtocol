using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.WorldState
{
    public class CreateCharacterNode : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.CreateCharacterNode;

        public byte NodeType { get; set; }
        public byte ControllerNodetype { get; set; }
        public byte Flags { get; set; }

        public CreateCharacterNode(byte nodeType, byte controllerNodetype, byte flags)
        {
            this.NodeType = nodeType;
            this.ControllerNodetype = controllerNodetype;
            this.Flags = flags;
        }

        public CreateCharacterNode(BinaryReader br)
        {
            NodeType = br.ReadByte();
            ControllerNodetype = br.ReadByte();
            Flags = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(NodeType);
                    bw.Write(ControllerNodetype);
                    bw.Write(Flags);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::CreateCharacterNode:\n" +
                   $"  {nameof(NodeType)} = {NodeType}\n" +
                   $"  {nameof(ControllerNodetype)} = {ControllerNodetype}\n" +
                   $"  {nameof(Flags)} = {Flags}\n";
        }
    }
}
