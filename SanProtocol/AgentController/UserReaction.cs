using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class UserReaction : IPacket
    {
        public uint MessageId => Messages.AgentController.UserReaction;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public string Type { get; set; }
        public List<float> Position { get; set; } = new List<float>();
        public Quaternion Orientation { get; set; } = new Quaternion();

        public UserReaction(ulong frame, uint agentControllerId, string type, List<float> position, Quaternion orientation)
        {
            this.Frame = frame;
            this.AgentControllerId = agentControllerId;
            this.Type = type;
            this.Position = position;
            this.Orientation = orientation;
        }

        public UserReaction(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            Type = br.ReadSanString();

            var bitReader = new BitReader(br, 3 * 26 + (3 * 13 + 4));
            Position = bitReader.ReadFloats(3, 26, 2048.0f);
            Orientation = bitReader.ReadQuaternion(3, 13);
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
                    bw.WriteSanString(Type);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteFloats(Position, 26, 2048.0f);
                    bitWriter.WriteQuaternion(Orientation, 13);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::UserReaction:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Type)} = {Type}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(Orientation)} = {Orientation}\n";
        }
    }
}
