using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class CharacterIKBone : IPacket
    {
        public uint MessageId => Messages.AgentController.CharacterIKBone;

        public byte BoneIndex { get; set; }
        public Quaternion LocalOrientation { get; set; } = new Quaternion();

        public CharacterIKBone(byte boneIndex, Quaternion localOrientation)
        {
            this.BoneIndex = boneIndex;
            this.LocalOrientation = localOrientation;
        }

        public CharacterIKBone(BinaryReader br)
        {
            var bitReader = new BitReader(br, 6 + (3*12 + 4));
            BoneIndex = (byte)bitReader.ReadUnsigned(6);
            LocalOrientation = bitReader.ReadQuaternion(3, 12);
        }

        public byte[] GetBytes()
        {
            var bitWriter = new BitWriter();
            bitWriter.WriteFloat(BoneIndex, 6, 1.0f);
            bitWriter.WriteQuaternion(LocalOrientation, 12);
            var bits = bitWriter.GetBytes();

            return bits;
        }

        public override string ToString()
        {
            return $"AgentController::CharacterIKBone:\n" +
                   $"  {nameof(BoneIndex)} = {BoneIndex}\n" +
                   $"  {nameof(LocalOrientation)} = <{LocalOrientation}>\n";
        }
    }
}
