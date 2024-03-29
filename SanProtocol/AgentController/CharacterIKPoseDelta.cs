﻿namespace SanProtocol.AgentController
{
    public class CharacterIKPoseDelta : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.CharacterIKPoseDelta;

        public uint AgentControllerId { get; set; }
        public ulong Frame { get; set; }
        public Dictionary<byte, Quaternion> BoneRotations { get; set; } = new Dictionary<byte, Quaternion>();
        public List<float> RootBoneTranslationDelta { get; set; } = new List<float>();

        public CharacterIKPoseDelta(uint agentControllerId, ulong frame, Dictionary<byte, Quaternion> boneRotations, List<float> rootBoneTranslationDelta)
        {
            AgentControllerId = agentControllerId;
            Frame = frame;
            BoneRotations = boneRotations;
            RootBoneTranslationDelta = rootBoneTranslationDelta;
        }

        public CharacterIKPoseDelta(BinaryReader br)
        {
            BoneRotations = new Dictionary<byte, Quaternion>();

            AgentControllerId = br.ReadUInt32();
            Frame = br.ReadUInt64();
            var numBoneRotations = br.ReadUInt32();

            var bitReader = new BitReader(br);
            for (var i = 0; i < numBoneRotations; i++)
            {
                var boneIndex = (byte)bitReader.ReadUnsigned(6);
                var localOrientation = bitReader.ReadQuaternion(3, 7);
                BoneRotations[boneIndex] = localOrientation;
            }

            RootBoneTranslationDelta = bitReader.ReadFloats(3, 9, 0.1f);
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
                        bitWriter.WriteUnsigned(item.Key, 6);
                        bitWriter.WriteQuaternion(item.Value, 7);
                    }

                    bitWriter.WriteFloats(RootBoneTranslationDelta, 9, 0.1f);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::CharacterIKPoseDelta:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(BoneRotations)} = {string.Join(',', BoneRotations)}\n";
        }
    }

}
