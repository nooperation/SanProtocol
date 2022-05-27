using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class RequestWarpCharacter : WarpCharacter
    {
        public override uint MessageId => Messages.AgentController.RequestWarpCharacter;

        public RequestWarpCharacter(ulong frame, uint agentControllerId, float position_x, float position_y, float position_z, float rotation_x, float rotation_y, float rotation_z, float rotation_w)
            : base(frame, agentControllerId, position_x, position_y, position_z, rotation_x, rotation_y, rotation_z, rotation_w)
        {
        }

        public RequestWarpCharacter(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"AgentController::RequestWarpCharacter:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Position_x)} = {Position_x}\n" +
                   $"  {nameof(Position_y)} = {Position_y}\n" +
                   $"  {nameof(Position_z)} = {Position_z}\n" +
                   $"  {nameof(Rotation_x)} = {Rotation_x}\n" +
                   $"  {nameof(Rotation_y)} = {Rotation_y}\n" +
                   $"  {nameof(Rotation_z)} = {Rotation_z}\n" +
                   $"  {nameof(Rotation_w)} = {Rotation_w}\n";
        }
    }
}
