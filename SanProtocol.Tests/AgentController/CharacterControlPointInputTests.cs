using SanProtocol.AgentController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanProtocol.Tests.AgentController
{
    public class CharacterControlPointInputTests
    {
        [Fact]
        public void ReadWriteTest1()
        {
            byte[] rawInput = {
                0x22, 0xDA, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00, 0x21, 0xA2, 0xFF, 0x7F, 0xB3, 0x32, 0xFB, 0x7F,
                0xFF, 0xF7, 0x7F, 0xF3, 0xEF, 0xEF, 0xEF, 0x0F, 0x00, 0xC0
            };

            using (BinaryReader br = new BinaryReader(new MemoryStream(rawInput)))
            {
                // 2 don't care bits
                rawInput[rawInput.Length - 1] &= 0b00111111;

                var inputObject = new CharacterControlPointInput(br);
                var inputBytes = inputObject.GetBytes().Skip(4);
                Assert.Equal(rawInput, inputBytes);

                var outputObject = new CharacterControlPointInput(
                    inputObject.Frame,
                    inputObject.AgentControllerId,
                    inputObject.ControlPoints,
                    inputObject.LeftIndexTrigger,
                    inputObject.RightIndexTrigger,
                    inputObject.LeftGripTrigger,
                    inputObject.RightGripTrigger,
                    inputObject.LeftTouches,
                    inputObject.RightTouches,
                    inputObject.IndexTriggerControlsHand,
                    inputObject.LeftHandIsHolding,
                    inputObject.RightHandIsHolding
                );
                var outputBytes = outputObject.GetBytes().Skip(4);
                Assert.Equal(rawInput, outputBytes);

                var messageId = BitConverter.ToUInt32(inputObject.GetBytes(), 0);
                Assert.Equal(Messages.AgentController.CharacterControlPointInput, messageId);
            }
        }
        
        [Fact]
        public void ReadWriteTest2()
        {
            byte[] rawInput = {
                0x22, 0xFE, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x00, 0x00, 0xEE, 0x73, 0xDB, 0x71, 0xA5, 0x29, 0xD2, 0xBB,
                0x15, 0xA9, 0x8F, 0x29, 0x44, 0xF4, 0xFF, 0x6F, 0x56, 0x66, 0xFF, 0xEF,
                0xFF, 0xFE, 0x6F, 0xFE, 0xFD, 0xFD, 0xE5, 0x03, 0x00, 0xB0
            };

            using (BinaryReader br = new BinaryReader(new MemoryStream(rawInput)))
            {
                // 5 don't care bits
                rawInput[rawInput.Length - 1] &= 0b00000111;

                var inputObject = new CharacterControlPointInput(br);
                var inputBytes = inputObject.GetBytes().Skip(4);
                Assert.Equal(rawInput, inputBytes);

                var outputObject = new CharacterControlPointInput(
                    inputObject.Frame,
                    inputObject.AgentControllerId,
                    inputObject.ControlPoints,
                    inputObject.LeftIndexTrigger,
                    inputObject.RightIndexTrigger,
                    inputObject.LeftGripTrigger,
                    inputObject.RightGripTrigger,
                    inputObject.LeftTouches,
                    inputObject.RightTouches,
                    inputObject.IndexTriggerControlsHand,
                    inputObject.LeftHandIsHolding,
                    inputObject.RightHandIsHolding
                );
                var outputBytes = outputObject.GetBytes().Skip(4);
                Assert.Equal(rawInput, outputBytes);

                var messageId = BitConverter.ToUInt32(inputObject.GetBytes(), 0);
                Assert.Equal(Messages.AgentController.CharacterControlPointInput, messageId);
            }
        }
    }
}
