using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class CharacterIKPose : IPacket
    {
        public uint MessageId => Messages.AgentController.CharacterIKPose;

        public uint AgentControllerId { get; set; }
        public ulong Frame { get; set; }
        public Dictionary<byte, Quaternion> BoneRotations { get; set; } = new Dictionary<byte, Quaternion>();
        public List<float> RootBoneTranslation { get; set; } = new List<float>();

        public CharacterIKPose(uint agentControllerId, ulong frame, Dictionary<byte, Quaternion> boneRotations)
        {
            this.AgentControllerId = agentControllerId;
            this.Frame = frame;
            this.BoneRotations = boneRotations;
        }

        public CharacterIKPose(BinaryReader br)
        {
            BoneRotations = new Dictionary<byte, Quaternion>();

            AgentControllerId = br.ReadUInt32();
            Frame = br.ReadUInt64();
            var numBoneRotations = br.ReadUInt32();

            var bitReader = new BitReader(br, (int)numBoneRotations * (6 + (3 * 12 + 4)) + 3 * 14);
            for (int i = 0; i < numBoneRotations; i++)
            {
                var boneIndex = (byte)bitReader.ReadUnsigned(6);
                var localOrientation = bitReader.ReadQuaternion(3, 12);
                BoneRotations[boneIndex] = localOrientation;
            }

            RootBoneTranslation = bitReader.ReadFloats(3, 14, 3.0f);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(AgentControllerId);
                    bw.Write(Frame);

                    bw.Write(BoneRotations.Count);

                    var bitWriter = new BitWriter();
                    foreach (var item in BoneRotations)
                    {
                        bitWriter.WriteFloat(item.Key, 6, 1.0f);
                        bitWriter.WriteQuaternion(item.Value, 12);
                    }
                    bitWriter.WriteFloats(RootBoneTranslation, 14, 3.0f);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::CharacterIKPose:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(BoneRotations)} = {BoneRotations}\n" +
                   $"  {nameof(RootBoneTranslation)} = <{string.Join(',', RootBoneTranslation)}>\n";
        }
    }
}
