﻿using SanProtocol.AgentController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanProtocol.Tests.AgentController
{
    public class CharacterIKPoseTests
    {
        [Fact]
        public void ReadWriteTest1()
        {
            byte[] rawInput = {
                0x09, 0x00, 0x00, 0x00, 0xF5, 0x58, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x14, 0x00, 0x00, 0x00, 0xC4, 0xFC, 0x09, 0x5F, 0x97, 0xB6, 0xB1, 0x66,
                0x09, 0x94, 0x2C, 0x76, 0x6C, 0xFA, 0x0A, 0x5C, 0xAB, 0x21, 0x7B, 0xBD,
                0xF7, 0x64, 0x67, 0xDC, 0x81, 0x2C, 0xB4, 0xA9, 0x76, 0x37, 0x0C, 0x88,
                0x9E, 0x08, 0xE8, 0x8D, 0x7A, 0x56, 0xDE, 0x33, 0x36, 0xF7, 0x82, 0x0B,
                0x06, 0x7B, 0xE7, 0x85, 0x69, 0x05, 0xE2, 0x22, 0xFA, 0x8D, 0x55, 0x3A,
                0x73, 0x93, 0x8E, 0x16, 0x6B, 0xB1, 0x47, 0x6A, 0x76, 0x71, 0xCB, 0x87,
                0xEE, 0x65, 0xF5, 0x52, 0x8B, 0xB2, 0xA0, 0x33, 0x39, 0x77, 0x6D, 0x02,
                0x08, 0x39, 0xBD, 0x31, 0x11, 0x6A, 0x4E, 0xCF, 0x7F, 0xD9, 0xEB, 0x7F,
                0x54, 0x75, 0xDE, 0xED, 0x55, 0x41, 0x85, 0x79, 0xA6, 0x6F, 0x61, 0x81,
                0x7D, 0x07, 0xE6, 0x2C, 0x02, 0x66, 0x75, 0x7E, 0x55, 0x78, 0x03, 0xE6,
                0x9F, 0xFD, 0x27, 0x0E, 0x84
            };

            using (BinaryReader br = new BinaryReader(new MemoryStream(rawInput)))
            {
                // 6 don't care bits
                rawInput[rawInput.Length - 1] &= 0b00000011;

                var inputObject = new CharacterIKPose(br);
                var inputBytes = inputObject.GetBytes().Skip(4);

                Assert.Equal(rawInput, inputBytes);

                var outputObject = new CharacterIKPose(
                    inputObject.AgentControllerId,
                    inputObject.Frame,
                    inputObject.BoneRotations,
                    inputObject.RootBoneTranslation
                );
                var outputBytes = outputObject.GetBytes().Skip(4);
                Assert.Equal(rawInput, outputBytes);

                var messageId = BitConverter.ToUInt32(inputObject.GetBytes(), 0);
                Assert.Equal(Messages.AgentController.CharacterIKPose, messageId);
            }
        }
    }
}
