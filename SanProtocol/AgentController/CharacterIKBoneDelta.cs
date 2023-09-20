using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class CharacterIKBoneDelta : IPacket
    {
        public uint MessageId => Messages.AgentControllerMessages.CharacterIKBoneDelta;

        public byte BoneIndex { get; set; }
        public Quaternion LocalOrientation { get; set; } = new Quaternion();

        public CharacterIKBoneDelta(byte boneIndex, Quaternion localOrientation)
        {
            this.BoneIndex = boneIndex;
            this.LocalOrientation = localOrientation;
        }

        public CharacterIKBoneDelta(BinaryReader br)
        {
            var bitReader = new BitReader(br, 6 + (3 * 7 + 4));
            BoneIndex = (byte)bitReader.ReadUnsigned(6);
            LocalOrientation = bitReader.ReadQuaternion(3, 7);
        }

        public byte[] GetBytes()
        {
            var bitWriter = new BitWriter();
            bitWriter.WriteFloat(BoneIndex, 6, 1.0f);
            bitWriter.WriteQuaternion(LocalOrientation, 7);
            var bits = bitWriter.GetBytes();

            return bits;
        }

        public override string ToString()
        {
            return $"AgentController::CharacterIKBoneDelta:\n" +
                   $"  {nameof(BoneIndex)} = {BoneIndex}\n" +
                   $"  {nameof(LocalOrientation)} = <{String.Join(',', LocalOrientation)}>\n";
        }
    }
}
